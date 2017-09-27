using System;
using System.Globalization;
using System.Runtime.Serialization;
//using System.Security.Permissions;

namespace DFF.Freedom.Core.Exception
{
	/// <summary>
	/// 在向方法提供的其中一个参数无效时引发的异常。
	/// </summary>
	[System.Serializable]
	public abstract class ParaException : System.ArgumentException
	{
		#region 属性

		/// <summary>
		/// 获取异常信息格式化字符串。
		/// </summary>
		protected abstract string FormatString { get; }

		#endregion

		#region 构造函数

		/// <summary>
		/// 使用方法参数名称初始化 <see cref="ParaException"/> 类的新实例。
		/// </summary>
		/// <param name="paraName">方法参数名称。</param>
		protected ParaException(string paraName)
			: base(System.String.Empty, paraName)
		{
		}

		#endregion

		#region System.ArgumentException 成员

		/// <summary>
		/// 获取描述当前异常的消息。
		/// </summary>
		public override string Message
		{
			get
			{
				return System.String.Format(CultureInfo.InvariantCulture,
									 this.FormatString, base.ParamName);
			}
		}

        // TODO: .NET Core 1.0没有SecurityPermission
		///// <summary>
		///// 用关于异常的信息设置 <see cref="SerializationInfo"/>。
		///// </summary>
		///// <param name="info"><see cref="SerializationInfo"/>，它存有有关所引发异常的序列化的对象数据。</param>
		///// <param name="context"><see cref="StreamingContext"/>，它包含有关源或目标的上下文信息。</param>
		//[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		//public override void GetObjectData(SerializationInfo info, StreamingContext context)
		//{
  //          #region 参数验证

  //          if (info == null)
  //              throw new ArgumentException("info"); //throw ParaUtility.CanNotNullObject("info");

		//	#endregion

		//	base.GetObjectData(info, context);
		//	info.AddValue("FormatString", this.FormatString, typeof(string));
		//}

		#endregion
	}
}
