using DFF.Freedom.Core.Attribute;
using DFF.Freedom.Core.Exception;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace DFF.Freedom.Core.Extension
{
	/// <summary>
	/// 枚举描述项扩展方法静态类。
	/// </summary>
	public static class EnumDescriptionExtension
	{
		#region 获取附加信息

		/// <summary>
		/// 获取符合附加信息关键字的所有枚举项附加信息字符串。
		/// </summary>
		/// <param name="enumType">枚举类型。</param>
		/// <param name="key">附加信息关键字。</param>
		/// <returns>
		/// 包含枚举项名称、枚举项值及相对应枚举项附加信息的 <see cref="Triple{T1, T2, T3}"/> 结构集合。
		/// 第一个对象存储枚举项名称；第二个对象存储枚举项值；第三个对象存储枚举项附加信息。
		/// </returns>
		/// <exception cref="ParaNullException">
		/// 当参数 <paramref name="enumType"/> 接收空引用(<c>null</c>)时引发异常。
		/// 当参数 <paramref name="key"/> 接收空引用(<c>null</c>)时引发异常。
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// 当给定枚举类型中的枚举项未定义附加信息时引发异常。
		/// 当给定枚举类型中的枚举项上使用重复关键字定义了附加信息时引发异常。
		/// </exception>
		public static IEnumerable<Triple<String, Int32, String>> GetDescriptions(Type enumType, string key)
		{
			#region 参数验证

			if (enumType == null)
                throw new ArgumentNullException("enumType"); //throw ParaUtility.CanNotNullObject("enumType");
            if (key == null)               
                throw new ArgumentNullException("key");  //throw ParaUtility.CanNotNullObject("key");

            System.Exception e = EnumTypeNotMatch("enumType", enumType); //ParaUtility.EnumTypeNotMatch("enumType", enumType);
            if (e != null) throw e;

			#endregion

			FieldInfo[] fis = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);

			var triples =
				from fi in fis
				let enumItemValue = Convert.ToInt32(Enum.Parse(enumType, fi.Name, true), CultureInfo.InvariantCulture)
				let attrs = fi.GetCustomAttributes(typeof (EnumDescriptionAttribute), false)
				from EnumDescriptionAttribute attr in attrs
				where attr.Key == key
				select new Triple<String, Int32, String>(fi.Name, enumItemValue, attr.Description);

			#region 验证枚举项附加信息定义的正确性

			// 如果枚举项未定义附加信息。
			if (!triples.Any())
				throw new InvalidOperationException(
					String.Format(CultureInfo.CurrentCulture,
						Properties.Resources.EnumDescriptionInfoNotExistExceptionMsg,
						enumType.Name));

			// 如果枚举项的附加信息定义使用了重复的关键字。
			var repeatItems = from triple in triples
			                  group triple by triple.First
			                  into groupTriples
			                  where groupTriples.Count() > 1
			                  select groupTriples.Key;
			if (repeatItems.Any())
			{
				string enumItemNameStr = String.Join(",", repeatItems);

				throw new InvalidOperationException(
					String.Format(CultureInfo.CurrentCulture,
						Properties.Resources.EnumDescriptionInfoRepeatExceptionMsg,
						enumType.Name, enumItemNameStr, key));
			}
				

			#endregion

			return triples;
		}

		/// <summary>
		/// 获取所有枚举项的默认附加信息字符串。
		/// </summary>
		/// <param name="enumType">枚举类型。</param>
		/// <returns>
		/// 包含枚举项名称、枚举项值及相对应枚举项附加信息的 <see cref="Triple{T1, T2, T3}"/> 结构集合。
		/// 第一个对象存储枚举项名称；第二个对象存储枚举项值；第三个对象存储枚举项附加信息。
		/// </returns>
		public static IEnumerable<Triple<String, Int32, String>> GetDescriptions(Type enumType)
		{
			return GetDescriptions(enumType, String.Empty);
		}

		/// <summary>
		/// 根据给定枚举项，获取所有的附加信息。
		/// </summary>
		/// <param name="enumItem">枚举项。</param>
		/// <returns>
		/// 包含枚举项附加信息关键字、枚举项值及相对应枚举项附加信息的 <see cref="Triple{T1, T2, T3}"/> 结构集合。
		/// 第一个对象存储枚举项附加信息关键字；第二个对象存储枚举项值；第三个对象存储枚举项附加信息。
		/// </returns>
		/// <exception cref="InvalidOperationException">
		/// 当给定枚举类型中的枚举项未定义附加信息时引发异常。
		/// 当给定枚举类型中的枚举项上使用重复关键字定义了附加信息时引发异常。
		/// </exception>
		public static IEnumerable<Triple<String, Int32, String>> GetDescriptions(Enum enumItem)
		{
			Type enumType = enumItem.GetType();
			FieldInfo fi = enumType.GetField(enumItem.ToString());
			int enumItemValue = Convert.ToInt32(
				Enum.Parse(enumType, enumItem.ToString()), CultureInfo.InvariantCulture);

			var triples =
				from EnumDescriptionAttribute attr in fi.GetCustomAttributes(typeof (EnumDescriptionAttribute), false)
				select new Triple<String, Int32, String>(attr.Key, enumItemValue, attr.Description);

			#region 验证枚举项附加信息定义的正确性

			// 如果枚举项未定义附加信息。
			if (!triples.Any())
				throw new InvalidOperationException(
					String.Format(CultureInfo.CurrentCulture,
						Properties.Resources.EnumDescriptionInfoNotExistExceptionMsg,
						enumType.Name));

			// 如果枚举项的附加信息定义使用了重复的关键字。
			var repeatItems = from triple in triples
							  group triple by triple.First
								  into groupTriples
								  where groupTriples.Count() > 1
								  select groupTriples.Key;
			if (repeatItems.Any())
			{
				string enumItemKeyStr = String.Join(",", repeatItems);

				throw new InvalidOperationException(
					String.Format(CultureInfo.CurrentCulture,
						Properties.Resources.EnumDescriptionInfoRepeatExceptionMsg,
						enumType.Name, enumItem, enumItemKeyStr));
			}


			#endregion

			return triples;
		}

		/// <summary>
		/// 根据给定枚举项名称（值）及附加信息关键字，获取附加信息字符串。
		/// </summary>
		/// <param name="enumType">枚举类型。</param>
		/// <param name="value">枚举项名称（值）。</param>
		/// <param name="key">附加信息关键字。</param>
		/// <returns>返回附加信息字符串。</returns>
		/// <exception cref="StringParaEmptyOrNullException">
		/// 当参数 <paramref name="value"/> 接收空字符串（<see cref="String.Empty"/>）或空引用（<c>null</c>）时引发异常。
		/// </exception>
		/// <exception cref="ParaNullException">
		/// 当参数 <paramref name="enumType"/> 接收空引用(<c>null</c>)时引发异常。
		/// 当参数 <paramref name="key"/> 接收空引用(<c>null</c>)时引发异常。
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// 当给定枚举类型中的枚举项未定义附加信息时引发异常。
		/// </exception>
		public static string GetDescription(Type enumType, string value, string key)
		{
			#region 参数验证

			if (enumType == null)
                throw new ArgumentNullException("enumType");  //throw ParaUtility.CanNotNullObject("enumType");
            if (String.IsNullOrEmpty(value))
                throw new ArgumentNullException("key"); //throw ParaUtility.CanNotEmptyOrNullString("value");
			if (key == null)
                throw new ArgumentNullException("key");  //throw ParaUtility.CanNotNullObject("key");
			
			System.Exception e = EnumTypeNotMatch("enumType", enumType); //ParaUtility.EnumTypeNotMatch("enumType", enumType);
			if (e != null) throw e;

			#endregion

			Enum enumObj = (Enum)Enum.Parse(enumType, value, true);

			IEnumerable<Triple<String, Int32, String>> triples = GetDescriptions(enumObj);

			string description = (from triple in triples
			                      where triple.First == key
			                      select triple.Third).FirstOrDefault();

			if(String.IsNullOrEmpty(description))
				throw new InvalidOperationException(
					String.Format(CultureInfo.CurrentCulture,
						Properties.Resources.EnumDescriptionInfoNotExistExceptionMsg,
						enumType.Name));

			return description;
		}

		/// <summary>
		/// 根据给定枚举项名称（值）获取默认的附加信息字符串。
		/// </summary>
		/// <param name="enumType">枚举类型。</param>
		/// <param name="value">枚举项名称（值）。</param>
		/// <returns>附加信息字符串。</returns>
		public static string GetDescription(Type enumType, string value)
		{
			return GetDescription(enumType, value, String.Empty);
		}

		/// <summary>
		/// 根据给定枚举项及附加信息关键字，获取附加信息字符串。
		/// </summary>
		/// <param name="enumItem">枚举项。</param>
		/// <param name="key">附加信息关键字。</param>
		/// <returns>附加信息字符串。</returns>
		public static string GetDescription(Enum enumItem, string key)
		{
			return GetDescription(enumItem.GetType(), enumItem.ToString(), key);
		}

		/// <summary>
		/// 根据给定枚举项，获取默认的附加信息字符串。
		/// </summary>
		/// <param name="enumItem">枚举项。</param>
		/// <returns>附加信息字符串。</returns>
		public static string GetDescription(Enum enumItem)
		{
			return GetDescription(enumItem.GetType(), enumItem.ToString());
		}

		#endregion

		#region 获取枚举项

		/// <summary>
		/// 根据给定枚举值获取枚举项。
		/// </summary>
		/// <param name="enumType">枚举类型。</param>
		/// <param name="value">枚举名称或枚举值。</param>
		/// <returns>枚举项。</returns>
		/// <exception cref="ParaNullException">
		/// 当参数 <paramref name="enumType"/> 接收空引用(<c>null</c>)时引发异常。
		/// </exception>
		/// <exception cref="StringParaEmptyOrNullException">
		/// 当参数 <paramref name="value"/> 接收空字符串（<see cref="String.Empty"/>）或空引用（<c>null</c>）时引发异常。
		/// </exception>
		public static Enum GetEnumItem(Type enumType, string value)
		{
			#region 参数验证

			if (enumType == null)
                throw new ArgumentNullException("enumType"); //throw ParaUtility.CanNotNullObject("enumType");
			if (String.IsNullOrEmpty(value))
                throw new ArgumentNullException("value"); //throw ParaUtility.CanNotEmptyOrNullString("value");
			
			System.Exception e = EnumTypeNotMatch("enumType", enumType); //ParaUtility.EnumTypeNotMatch("enumType", enumType);
			if (e != null) throw e;

			#endregion

			return (Enum)Enum.Parse(enumType, value, true);
		}

		/// <summary>
		/// 根据给定枚举值获取枚举项。
		/// </summary>
		/// <typeparam name="TEnum">返回的枚举项类型。</typeparam>
		/// <param name="value">枚举值。</param>
		/// <returns>枚举项。</returns>
		public static TEnum GetEnumItem<TEnum>(string value) where TEnum : struct
		{
			object enumItemObj = GetEnumItem(typeof(TEnum), value);

			return (TEnum)enumItemObj;
		}

		/// <summary>
		/// 根据指定的默认枚举项附加信息获取对应的枚举项。
		/// </summary>
		/// <typeparam name="TEnum">枚举类型。</typeparam>
		/// <param name="description">附加信息字符串。</param>
		/// <returns>定义指定默认枚举项附加信息的枚举项。</returns>
		public static TEnum GetEnumItemByDescription<TEnum>(string description) where TEnum : struct
		{
			return EnumDescriptionExtension.GetEnumItemByDescription<TEnum>(String.Empty, description);
		}

		/// <summary>
		/// 根据指定的枚举项附加信息获取对应的枚举项。
		/// </summary>
		/// <typeparam name="TEnum">枚举类型。</typeparam>
		/// <param name="key">附加信息关键字。</param>
		/// <param name="description">附加信息字符串。</param>
		/// <returns>定义指定枚举项附加信息的枚举项。</returns>
		public static TEnum GetEnumItemByDescription<TEnum>(string key, string description) where TEnum : struct
		{
			return (TEnum) (object) EnumDescriptionExtension.GetEnumItemByDescription(typeof (TEnum), key, description);
		}

		/// <summary>
		/// 根据指定的默认枚举项附加信息获取对应的枚举项。
		/// </summary>
		/// <param name="enumType">枚举类型。</param>
		/// <param name="description">附加信息字符串。</param>
		/// <returns>定义指定默认枚举项附加信息的枚举项。</returns>
		public static Enum GetEnumItemByDescription(Type enumType, string description)
		{
			return EnumDescriptionExtension.GetEnumItemByDescription(enumType, String.Empty, description);
		}

		/// <summary>
		/// 根据指定的枚举项附加信息获取对应的枚举项。
		/// </summary>
		/// <param name="enumType">枚举类型。</param>
		/// <param name="key">附加信息关键字。</param>
		/// <param name="description">附加信息字符串。</param>
		/// <returns>定义指定枚举项附加信息的枚举项。</returns>
		public static Enum GetEnumItemByDescription(Type enumType, string key, string description)
		{
			#region 参数验证

			if (String.IsNullOrEmpty(description))
                throw new ArgumentNullException("description"); //throw ParaUtility.CanNotEmptyOrNullString("description");

			#endregion

			IEnumerable<Triple<String, Int32, String>> descriptions = 
				EnumDescriptionExtension.GetDescriptions(enumType, key);

			Triple<String, Int32, String> tmpTriple = descriptions.FirstOrDefault(triple => triple.Third == description);

			return EnumDescriptionExtension.GetEnumItem(
				enumType, tmpTriple.Second.ToString(CultureInfo.InvariantCulture));
		}

		#endregion

		#region 获取枚举值

		/// <summary>
		/// 根据给定枚举项，获取枚举值。
		/// </summary>
		/// <param name="enumItem">枚举项。</param>
		/// <returns>枚举值。</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062")]
		public static int GetEnumValue(Enum enumItem)
		{
			return int.Parse(Enum.Format(enumItem.GetType(), enumItem, "D"), CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// 根据给定枚举类型及枚举项名称，获取枚举值。
		/// </summary>
		/// <param name="enumType">枚举类型。</param>
		/// <param name="enumItem">枚举项名称。</param>
		/// <returns>枚举值。</returns>
		public static int GetEnumValue(Type enumType, string enumItem)
		{
			return GetEnumValue((Enum)Enum.Parse(enumType, enumItem));
		}

		#endregion

		#region 扩展方法

		/// <summary>
		/// 获取当前枚举项的默认描述字符串。
		/// </summary>
		/// <param name="enumItem">当前枚举项。</param>
		/// <returns>默认描述字符串。</returns>
		public static string GetDescriptionEx(this Enum enumItem)
		{
			return GetDescription(enumItem);
		}

		/// <summary>
		/// 根据指定关键字获取当前枚举项的描述字符串。
		/// </summary>
		/// <param name="enumItem">当前枚举项。</param>
		/// <param name="key">描述关键字。</param>
		/// <returns>描述字符串。</returns>
		public static string GetDescriptionEx(this Enum enumItem, string key)
		{
			return GetDescription(enumItem, key);
		}

		/// <summary>
		/// 获取指定枚举项的整数值。
		/// </summary>
		/// <param name="enumItem">枚举项。</param>
		/// <returns>对应的整数值。</returns>
		public static int GetEnumValueEx(this Enum enumItem)
		{
			return EnumDescriptionExtension.GetEnumValue(enumItem);
		}

		#endregion
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paraName"></param>
        /// <param name="paraValue"></param>
        /// <returns></returns>
        public static System.Exception EnumTypeNotMatch(string paraName, Type paraValue)
        {
            #region 参数验证

            if (String.IsNullOrEmpty(paraName))
                throw new ArgumentNullException("paraName"); //throw CanNotEmptyOrNullString("paraName");
            if (paraName == null)
                throw new ArgumentNullException("paraValue"); //throw CanNotNullObject("paraValue");

            #endregion

            // TODO: .NET Core没有IsEnum
            //return paraValue.IsEnum ? null : new EnumParaNotMatchException(paraName);

            return null;
        }
    }
}
