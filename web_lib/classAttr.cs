using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace web_lib
{
    public static class classAttr
    {



        public static string ToDescriptionAttr<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            System.ComponentModel.DescriptionAttribute[] attributes = (System.ComponentModel.DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(System.ComponentModel.DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }


        public static MemberInfo GetPropertyInformation(Expression propertyExpression)
        {
         //   Debug.Assert(propertyExpression != null, "propertyExpression != null");
            MemberExpression memberExpr = propertyExpression as MemberExpression;
            if (memberExpr == null)
            {
                UnaryExpression unaryExpr = propertyExpression as UnaryExpression;
                if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
                {
                    memberExpr = unaryExpr.Operand as MemberExpression;
                }
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
            {
                return memberExpr.Member;
            }

            return null;
        }

        public static string GetAttributeName<T>(this T itm, Expression<Func<T, object>> propertyExpression)
        {
            var memberInfo = GetPropertyInformation(propertyExpression.Body);
            if (memberInfo == null)
            {
                throw new ArgumentException(
                    "No property reference expression was found.",
                    "propertyExpression");
            }

            var pi = typeof(T).GetProperty(memberInfo.Name);
            var ret = pi.GetCustomAttributes(typeof(DisplayAttribute), true).Cast<DisplayAttribute>().SingleOrDefault();
            return ret != null ? ret.Name : pi.Name;
        }

            public static KPvalusAttribute ToKPvalusAttr<T>(this T source) where T : Enum
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            KPvalusAttribute[] attributes = (KPvalusAttribute[])fi.GetCustomAttributes(
                typeof(KPvalusAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0];
            else return null;
        }
        public static KPColorAttribute ToKPColorAttr<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            KPColorAttribute[] attributes = (KPColorAttribute[])fi.GetCustomAttributes(
                typeof(KPColorAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0];
            else return null;
        }




        [System.AttributeUsage(System.AttributeTargets.Field)]
        public class KPvalusAttribute : System.Attribute
        {
            public string Description;
            public string Description2;

            public bool IsActive;

            public KPvalusAttribute()
            {


            }
        }


        [System.AttributeUsage(System.AttributeTargets.Field)]
        public class KPColorAttribute : System.Attribute
        {
            public string ClassColor;
            public string ClassIcon;



        }

    }
}
