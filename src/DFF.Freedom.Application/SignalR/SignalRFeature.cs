namespace DFF.Freedom.SignalR
{
    /// <summary>
    /// SignalR特征
    /// </summary>
    public static class SignalRFeature
    {
        /// <summary>
        /// 是否激活
        /// </summary>
        public static bool IsAvailable
        {
            get
            {
#if FEATURE_SIGNALR
                return true;
#else
                return false;
#endif
            }
        }
    }
}
