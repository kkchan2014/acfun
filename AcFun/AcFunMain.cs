using AcFun.fn;
using AcFun.fn.Player;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace AcFun
{
    public partial class AcFunMain : Form
    {
        private fn.XAcFun AcFun = new fn.XAcFun();
        private SearchResult Result = null;
        private VideoInfo CurrentVideoInfo = null;

        public AcFunMain()
        {
            InitializeComponent();
            axShockwaveFlash1.FlashCall += axShockwaveFlash1_FlashCall;
        }

        private void txtKeyword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                labSearch.Focus();
                labSearch_Click(null, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = false;
            }
        }

        /// <summary>
        /// 优酷Swf播放器播放时会Call Javascript，执行不了Javascript则不能播放
        /// 此处做的是模拟执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axShockwaveFlash1_FlashCall(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEvent e)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(e.request);

            var req = doc.FirstChild.Attributes["name"].Value;

            switch (req)
            {
                case "isNaN":
                    axShockwaveFlash1.SetReturnValue("<boolean>true</boolean>");
                    break;
                case "function () { return document.location.href; }":
                    axShockwaveFlash1.SetReturnValue(String.Format("<string>http://www.acfun.tv/v/{0}</string>", CurrentVideoInfo.contentId));
                    break;
                case "function () { return document.referrer; }":
                    axShockwaveFlash1.SetReturnValue("<string></string>");
                    break;
                case "function () { return navigator.userAgent; }":
                    axShockwaveFlash1.SetReturnValue("<string>Mozilla/5.0 (Windows NT 10.0; WOW64; rv:45.0) Gecko/20100101 Firefox/45.0</string>");
                    break;
                case "hasCrashLogger":
                    axShockwaveFlash1.SetReturnValue("<boolean>true</boolean>");
                    break;
                case "eval":
                    break;
                case "player.store.onload":
                    break;
                case "onPlayerStatusChange":
                    break;
            }
        }

        private void labSearch_Click(object sender, EventArgs e)
        {
            Search(1);
        }

        private void Search(int pageIndex)
        {
            if (pageIndex == 1) trvResult.Nodes.Clear();

            Result = AcFun.Search(txtKeyword.Text.Trim(), pageIndex);

            if (Result != null && Result.success && Result.data.page.list != null)
            {
                foreach (var v in Result.data.page.list)
                {
                    trvResult.Nodes.Add(new TreeNode()
                    {
                        Text = "[视频]" + v.title,
                        ToolTipText = v.title,
                        Tag = v
                    });
                }

                if (Result.data.page.totalPage > Result.data.page.pageNo)
                {
                    trvResult.Nodes.Add(new TreeNode()
                    {
                        Text = "加载更多..."
                    });
                }
            }
            else if (pageIndex == 1) 
            {
                trvResult.Nodes.Add(new TreeNode()
                {
                    Text = "[无结果]"
                });
            }
        }

        private void trvResult_DoubleClick(object sender, EventArgs e)
        {
            var node = trvResult.SelectedNode;

            if (node == null || node.Text == "[无结果]") return;

            if (node.Text.StartsWith("加载更多..."))
            {
                node.Remove();

                Search(Result.data.page.pageNo + 1);
            }
            else if (node.Text.StartsWith("[视频]"))
            {
                if (node.Nodes.Count == 0)
                {
                    var video = AcFun.GetVideo(node.Tag as Detail);

                    if (video != null && video.Parts.Count > 0)
                    {
                        var index = 0;

                        foreach (var part in video.Parts)
                        {
                            var name = String.IsNullOrEmpty(part.Name) ? video.Detail.title : part.Name;

                            node.Nodes.Add(new TreeNode()
                            {
                                Text = (++index) + "、" + name,
                                ToolTipText = name,
                                Tag = part
                            });
                        }
                    }
                    else
                    {
                        node.Nodes.Add(new TreeNode()
                        {
                            Text = "[无结果]"
                        });
                    }

                    node.Toggle();
                }
            }
            else
            {
                CurrentVideoInfo = AcFun.GetVideoInfo(node.Tag as VideoPart);

                if (CurrentVideoInfo != null && CurrentVideoInfo.videoList != null)
                {
                    var player = BasePlayer.Parse(CurrentVideoInfo, CurrentVideoInfo.videoList[0]);

                    axShockwaveFlash1.StopPlay();
                    axShockwaveFlash1.Stop();
                    axShockwaveFlash1.Movie = player.Url;
                    if (player.Parms != null)
                    {
                        foreach (string[] item in player.Parms)
                        {
                            axShockwaveFlash1.SetVariable(item[0], item[1]);
                        }
                    }

                    axShockwaveFlash1.Play();
                }
                else
                {
                    MessageBox.Show("无法播放视频.");
                }
            }
        }
    }
}