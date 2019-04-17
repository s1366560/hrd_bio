using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public class LoadImagUtility
    {
        private static readonly object ImageLock = new object();

        static Image suspend;
        /// <summary>
        /// 暂停按钮图
        /// </summary>
        public static Image Suspend
        {
            get
            {
                lock (ImageLock)
                {
                    if (suspend == null)
                    {
                        suspend = System.Drawing.Image.FromFile("..\\..\\Resources\\Image\\Tempstoping.png");
                    }
                    return suspend;
                }
            }
        }

        static Image riring;
        /// <summary>
        /// 启动按钮图
        /// </summary>
        public static Image Firing
        {
            get
            {
                lock (ImageLock)
                {
                    if (riring == null)
                    {
                        riring = System.Drawing.Image.FromFile("..\\..\\Resources\\Image\\Execing.png");
                    }
                    return riring;
                }
            }
        }

        static Image checkMeun;
        /// <summary>
        /// 界面的二级菜单点击选中显示的图
        /// </summary>
        public static Image CheckMeun
        {
            get
            {
                lock (ImageLock)
                {
                    if (checkMeun == null)
                    {
                        checkMeun = System.Drawing.Image.FromFile("..\\..\\Resources\\Image\\zhixiang.png");
                    }
                    return checkMeun;
                }
            }
        }

        static Image initialiseLISServer;
        /// <summary>
        /// 初始化LIS服务图
        /// </summary>
        public static Image InitialiseLISServer
        {
            get
            {
                lock (ImageLock)
                {
                    if (initialiseLISServer == null)
                    {
                        initialiseLISServer = System.Drawing.Image.FromFile("..\\..\\Resources\\Image\\LIS\\server.png");
                    }
                    return initialiseLISServer;
                }
            }
        }

        static Image clearLISServer;
        /// <summary>
        /// 清除LIS服务图
        /// </summary>
        public static Image ClearLISServer
        {
            get
            {
                lock (ImageLock)
                {
                    if (clearLISServer == null)
                    {
                        clearLISServer = System.Drawing.Image.FromFile("..\\..\\Resources\\Image\\LIS\\server_lightning.png");
                    }
                    return clearLISServer;
                }
            }
        }

        static Image connectioLISServerFailed;
        /// <summary>
        /// 连接LIS服务失败图
        /// </summary>
        public static Image ConnectLISServerFailed
        {
            get
            {
                lock (ImageLock)
                {
                    if (connectioLISServerFailed == null)
                    {
                        connectioLISServerFailed = System.Drawing.Image.FromFile("..\\..\\Resources\\Image\\LIS\\server_delete.png");
                    }
                    return connectioLISServerFailed;
                }
            }
        }

        static Image connectioLISServerSuccess;
        /// <summary>
        /// 连接LIS服务成功图
        /// </summary>
        public static Image ConnectioLISServerSuccess
        {
            get
            {
                lock (ImageLock)
                {
                    if (connectioLISServerSuccess == null)
                    {
                        connectioLISServerSuccess = System.Drawing.Image.FromFile("..\\..\\Resources\\Image\\LIS\\server_add.png");
                    }
                    return connectioLISServerSuccess;
                }
            }
        }

        static Image sendDateToLISServer;
        /// <summary>
        /// 发送数据给LIS服务图
        /// </summary>
        public static Image SendDateToLISServer
        {
            get
            {
                lock (ImageLock)
                {
                    if (sendDateToLISServer == null)
                    {
                        sendDateToLISServer = System.Drawing.Image.FromFile("..\\..\\Resources\\Image\\LIS\\server_go.png");
                    }
                    return sendDateToLISServer;
                }
            }
        }

        static Image normal;
        /// <summary>
        /// 提示：正常
        /// </summary>
        public static Image Normal
        {
            get
            {
                lock (ImageLock)
                {
                    if (normal == null)
                    {
                        normal = System.Drawing.Image.FromFile("..\\..\\Resources\\Image\\WarnUn.png");
                    }
                    return normal;
                }
            }
        }

        static Image warning;
        /// <summary>
        /// 提示：警告
        /// </summary>
        public static Image Warning
        {
            get
            {
                lock (ImageLock)
                {
                    if (warning == null)
                    {
                        warning = System.Drawing.Image.FromFile("..\\..\\Resources\\Image\\Warning.png");
                    }
                    return warning;
                }
            }
        }

        static Image erroring;
        /// <summary>
        /// 提示：错误
        /// </summary>
        public static Image Erroring
        {
            get
            {
                lock (ImageLock)
                {
                    if (erroring == null)
                    {
                        erroring = System.Drawing.Image.FromFile("..\\..\\Resources\\Image\\Erroring.png");
                    }
                    return erroring;
                }
            }
        }
    }
}
