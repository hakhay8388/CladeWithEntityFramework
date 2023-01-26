using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nApplication.nCoreLoggers;
//using Bootstrapper.Core.nApplication.nCoreLoggers.nMethodCallLogger;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nHandlers.nLambdaHandler
{
    public class cLambdaHandler : cCoreObject
    {
        public cLambdaHandler(nApplication.cApp _App)
            :base(_App)
        {
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cLambdaHandler>(this);
        }

        // syntaxes:
        // () => objectName                 : objectExpression
        // () => objectName.propName        : objectPropExpression
        // paramName => paramName.propName  : paramPropExpression

        public string GetObjectName<T>(Expression<Func<T>> _ObjectExpression) // () => objectName ... returns objectName
        {
            string __Name = "";

            MemberExpression __MemberExpression = _ObjectExpression.Body as MemberExpression;

            if (__MemberExpression != null)
            {
                __Name = __MemberExpression.Member.Name;
            }
            //else throw new CoreException(RS.Message.E1001_GetObjectNameTCouldNotBeParsed, objectExpression.ToString());

            return __Name;
        }
        public string GetObjectName(Expression<Func<object>> _ObjectPropExpression) // () => objectName.propName or () => objectName ... returns objectName
        {
            string __Alias = "";

            switch (_ObjectPropExpression.NodeType)
            {
                case ExpressionType.Lambda:
                    if (_ObjectPropExpression.Body.NodeType == ExpressionType.MemberAccess)
                    {
                        //()=>objectName.propName
                        MemberExpression __MemberExpression = (_ObjectPropExpression.Body as MemberExpression).Expression as MemberExpression;

                        //()=>objectName
                        if (__MemberExpression == null)
                            __MemberExpression = _ObjectPropExpression.Body as MemberExpression;

                        __Alias = __MemberExpression.Member.Name;
                    }
                    else
                        if (_ObjectPropExpression.Body.NodeType == ExpressionType.Convert)
                            __Alias = (((_ObjectPropExpression.Body as UnaryExpression).Operand as MemberExpression).Expression as MemberExpression).Member.Name;
                    break;
            }

            //if (string.IsNullOrEmpty(alias)) throw new CoreException(RS.Message.E1002_GetObjectNameCouldNotBeParsed, objectPropExpression.ToString());

            return __Alias;
        }
        public string GetObjectPropName(Expression<Func<object>> _ObjectPropExpression) // () => objectName.propName ... returns propName
        {
            string __Name = "";

            switch (_ObjectPropExpression.NodeType)
            {
                case ExpressionType.Lambda:
                    if (_ObjectPropExpression.Body.NodeType == ExpressionType.MemberAccess)
                        __Name = (_ObjectPropExpression.Body as MemberExpression).Member.Name; // if () => objectName return objectName
                    else
                        if (_ObjectPropExpression.Body.NodeType == ExpressionType.Convert)
                            __Name = ((_ObjectPropExpression.Body as UnaryExpression).Operand as MemberExpression).Member.Name;
                    break;
            }

            //if (string.IsNullOrEmpty(name)) throw new CoreException(RS.Message.E1003_GetObjectPropNameCouldNotBeParsed, objectPropExpression.ToString());

            return __Name;
        }

        public Type GetUnderlyingType(MemberInfo _Member)
        {
            switch (_Member.MemberType)
            {
                case MemberTypes.Event:
                    return ((EventInfo)_Member).EventHandlerType;
                case MemberTypes.Field:
                    return ((FieldInfo)_Member).FieldType;
                case MemberTypes.Method:
                    return ((MethodInfo)_Member).ReturnType;
                case MemberTypes.Property:
                    return ((PropertyInfo)_Member).PropertyType;
                default:
                    throw new ArgumentException
                    (
                     "Input MemberInfo must be if type EventInfo, FieldInfo, MethodInfo, or PropertyInfo"
                    );
            }
        }

        public Type GetObjectPropType(Expression<Func<object>> _ObjectPropExpression) // () => objectName.propName ... returns propName
        {
            Type __MemberType = null;

            switch (_ObjectPropExpression.NodeType)
            {
                case ExpressionType.Lambda:
                    if (_ObjectPropExpression.Body.NodeType == ExpressionType.MemberAccess)
                        __MemberType = GetUnderlyingType((_ObjectPropExpression.Body as MemberExpression).Member); // if () => objectName return objectName
                    else
                        if (_ObjectPropExpression.Body.NodeType == ExpressionType.Convert)
                            __MemberType = GetUnderlyingType(((_ObjectPropExpression.Body as UnaryExpression).Operand as MemberExpression).Member);
                    break;
            }

            //if (string.IsNullOrEmpty(name)) throw new CoreException(RS.Message.E1003_GetObjectPropNameCouldNotBeParsed, objectPropExpression.ToString());

            return __MemberType;
        }


        public string GetObjectNameAndPropName(Expression<Func<object>> _ObjectPropExpression) // () => objectName.propName ... returns objectName.propName
        {
            return GetObjectName(_ObjectPropExpression) + "." + GetObjectPropName(_ObjectPropExpression);
        }
        public string GetParamPropName<T>(Expression<Func<T, object>> _ParamPropExpression) // paramName => paramName.propName ... returns propName
        {
            string __Name = "";

            switch (_ParamPropExpression.NodeType)
            {
                case ExpressionType.Lambda:
                    if (_ParamPropExpression.Body.NodeType == ExpressionType.MemberAccess)
                        __Name = (_ParamPropExpression.Body as MemberExpression).Member.Name;
                    else
                        if (_ParamPropExpression.Body.NodeType == ExpressionType.Convert)
                            __Name = ((_ParamPropExpression.Body as UnaryExpression).Operand as MemberExpression).Member.Name;
                    break;
            }

            //if (string.IsNullOrEmpty(name)) throw new CoreException(RS.Message.E1004_GetParamPropNameCouldNotBeParsed, paramPropExpression.ToString());

            return __Name;
        }
        public Type GetParamPropType<T>(Expression<Func<T, object>> _ParamPropExpression) // paramName => paramName.propName ... returns typeof(propName)
        {
            MemberExpression __Body = _ParamPropExpression.Body as MemberExpression;

            if (__Body == null)
            {
                UnaryExpression ubody = (UnaryExpression)_ParamPropExpression.Body;
                __Body = ubody.Operand as MemberExpression;
            }

            return ((PropertyInfo)__Body.Member).PropertyType;
        }
        public Type GetObjectType(Expression<Func<object>> _ObjectPropExpression) // () => objectName.propName or () => objectName ... returns typeof(objectName)
        {
            switch (_ObjectPropExpression.NodeType)
            {
                case ExpressionType.Lambda:
                    if (_ObjectPropExpression.Body.NodeType == ExpressionType.MemberAccess)
                    {
                        //()=>objectName.propName
                        MemberExpression __MemberExpression = (_ObjectPropExpression.Body as MemberExpression).Expression as MemberExpression;

                        //()=>objectName
                        if (__MemberExpression == null)
                            __MemberExpression = _ObjectPropExpression.Body as MemberExpression;

                        return __MemberExpression.Type;
                    }
                    else
                        if (_ObjectPropExpression.Body.NodeType == ExpressionType.Convert)
                            return (((_ObjectPropExpression.Body as UnaryExpression).Operand as MemberExpression).Expression as MemberExpression).Type;
                    break;
            }

            return null;
        }
    }
}
