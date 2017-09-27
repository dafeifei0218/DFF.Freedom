namespace DFF.Freedom.Core
{
	/// <summary>
	/// 代表三个相关对象构成的结构。
	/// </summary>
	public struct Triple<T1,T2,T3>
	{
		#region 属性

		/// <summary>
		/// 获取或设置第一个对象。
		/// </summary>
		public T1 First { get; set; }

		/// <summary>
		/// 获取或设置第二个对象。
		/// </summary>
		public T2 Second { get; set; }

		/// <summary>
		/// 获取或设置第三个对象。
		/// </summary>
		public T3 Third { get; set; }

		#endregion

		#region 构造函数

		/// <summary>
		/// 初始化 <see cref="Triple{T1, T2, T3}"/> 结构。
		/// </summary>
		/// <param name="first">第一个对象。</param>
		/// <param name="second">第二个对象。</param>
		/// <param name="third">第三个对象。</param>
		public Triple(T1 first, T2 second, T3 third) : this()
		{
			this.First = first;
			this.Second = second;
			this.Third = third;
		}

		#endregion
	}
}
