namespace DFF.Freedom.Core.Exception
{
	/// <summary>
	/// 当将非枚举对象传递给不接受它作为有效参数的方法时引发的异常。
	/// </summary>
	[System.Serializable]
	public sealed class EnumParaNotMatchException : ParaException
	{
		#region 构造函数

		/// <summary>
		/// 使用方法参数名称初始化 <see cref="ParaException"/> 类的新实例。
		/// </summary>
		/// <param name="paraName">方法参数名称。</param>
		public EnumParaNotMatchException(string paraName) : base(paraName)
		{
		}

		#endregion

		#region ParaException 成员

		/// <summary>
		/// 获取异常信息格式化字符串。
		/// </summary>
		protected override string FormatString
		{
			get { return Properties.Resources.EnumParaNotMatchExceptionMsg; }
		}

		#endregion
	}
}
