using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using MilpManager.Abstraction;
using MilpManager.Implementation;
using MilpManager.Utilities;

namespace MilpOperatorsWrapper
{
	public class VariableWrapper: RealProxy, IRemotingTypeInfo, IVariable
	{
		public override IMessage Invoke(IMessage msg)
		{
			var methodCall = msg as IMethodCallMessage;

			if (methodCall == null) return null;

			var result = methodCall.MethodBase.Invoke(Wrapped, methodCall.InArgs);
			return new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);
		}

		public bool CanCastTo(Type fromType, object o)
		{
			return true;
		}

		public string TypeName { get; set; }

		protected IVariable WrappedVariable;

		/// <summary>
		/// Wrapped variable
		/// </summary>
		public IVariable Wrapped => (WrappedVariable as VariableWrapper)?.Wrapped ?? WrappedVariable;

		public VariableWrapper() { }

		public VariableWrapper(IVariable wrapped)
		{
			if (wrapped == null) throw new ArgumentNullException(nameof(wrapped));
			WrappedVariable = wrapped;
		}

		/// <summary>
		/// Performs addition
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of addition</returns>
		public static VariableWrapper operator +(VariableWrapper a, IVariable b)
		{
			return a.Wrapped.Operation<Addition>(b).Wrap();
		}

		/// <summary>
		/// Performs addition
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of addition</returns>
		public static VariableWrapper operator +(VariableWrapper a, VariableWrapper b)
		{
			return a.Wrapped.Operation<Addition>(b.Wrapped).Wrap();
		}

		/// <summary>
		/// Performs addition
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of addition</returns>
		public static VariableWrapper operator +(VariableWrapper a, int b)
		{
			return a.Wrapped.Operation<Addition>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}

		/// <summary>
		/// Performs addition
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of addition</returns>
		public static VariableWrapper operator +(VariableWrapper a, double b)
		{
			return a.Wrapped.Operation<Addition>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}

		/// <summary>
		/// Performs subtraction
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of subtraction</returns>
		public static VariableWrapper operator -(VariableWrapper a, IVariable b)
		{
			return a.Wrapped.Operation<Subtraction>(b).Wrap();
		}

		/// <summary>
		/// Performs subtraction
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of subtraction</returns>
		public static VariableWrapper operator -(VariableWrapper a, VariableWrapper b)
		{
			return a.Wrapped.Operation<Subtraction>(b.Wrapped).Wrap();
		}

		/// <summary>
		/// Performs subtraction
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of subtraction</returns>
		public static VariableWrapper operator -(VariableWrapper a, int b)
		{
			return a.Wrapped.Operation<Subtraction>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}

		/// <summary>
		/// Performs subtraction
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of subtraction</returns>
		public static VariableWrapper operator -(VariableWrapper a, double b)
		{
			return a.Wrapped.Operation<Subtraction>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}

		/// <summary>
		/// Performs multiplication
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of multiplication</returns>
		public static VariableWrapper operator *(VariableWrapper a, IVariable b)
		{
			return a.Wrapped.Operation<Multiplication>(b).Wrap();
		}

		/// <summary>
		/// Performs multiplication
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of multiplication</returns>
		public static VariableWrapper operator *(VariableWrapper a, VariableWrapper b)
		{
			return a.Wrapped.Operation<Multiplication>(b.Wrapped).Wrap();
		}

		/// <summary>
		/// Performs multiplication
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of multiplication</returns>
		public static VariableWrapper operator *(VariableWrapper a, int b)
		{
			return a.Wrapped.Operation<Multiplication>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}

		/// <summary>
		/// Performs multiplication
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of multiplication</returns>
		public static VariableWrapper operator *(VariableWrapper a, double b)
		{
			return a.Wrapped.Operation<Multiplication>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}

		/// <summary>
		/// Performs division
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of division</returns>
		public static VariableWrapper operator /(VariableWrapper a, IVariable b)
		{
			return a.Wrapped.Operation<RealDivision>(b).Wrap();
		}

		/// <summary>
		/// Performs division
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of division</returns>
		public static VariableWrapper operator /(VariableWrapper a, VariableWrapper b)
		{
			return a.Wrapped.Operation<RealDivision>(b.Wrapped).Wrap();
		}

		/// <summary>
		/// Performs division
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of division</returns>
		public static VariableWrapper operator /(VariableWrapper a, int b)
		{
			return a.Wrapped.Operation<RealDivision>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}

		/// <summary>
		/// Performs division
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of division</returns>
		public static VariableWrapper operator /(VariableWrapper a, double b)
		{
			return a.Wrapped.Operation<RealDivision>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}

		/// <summary>
		/// Performs modulo
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of modulo</returns>
		public static VariableWrapper operator %(VariableWrapper a, IVariable b)
		{
			return a.Wrapped.Operation<Remainder>(b).Wrap();
		}

		/// <summary>
		/// Performs modulo
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of modulo</returns>
		public static VariableWrapper operator %(VariableWrapper a, VariableWrapper b)
		{
			return a.Wrapped.Operation<Remainder>(b.Wrapped).Wrap();
		}

		/// <summary>
		/// Performs modulo
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of modulo</returns>
		public static VariableWrapper operator %(VariableWrapper a, int b)
		{
			return a.Wrapped.Operation<Remainder>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}

		/// <summary>
		/// Performs modulo
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of modulo</returns>
		public static VariableWrapper operator %(VariableWrapper a, double b)
		{
			return a.Wrapped.Operation<Remainder>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}

		/// <summary>
		/// Performs exclusive disjunction (XOR)
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of exclusive disjunction (XOR)</returns>
		public static VariableWrapper operator ^(VariableWrapper a, IVariable b)
		{
			return a.Wrapped.Operation<ExclusiveDisjunction>(b).Wrap();
		}

		/// <summary>
		/// Performs exclusive disjunction (XOR)
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of exclusive disjunction (XOR)</returns>
		public static VariableWrapper operator ^(VariableWrapper a, VariableWrapper b)
		{
			return a.Wrapped.Operation<ExclusiveDisjunction>(b.Wrapped).Wrap();
		}

		/// <summary>
		/// Performs exclusive disjunction (XOR)
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of exclusive disjunction (XOR)</returns>
		public static VariableWrapper operator ^(VariableWrapper a, int b)
		{
			return a.Wrapped.Operation<ExclusiveDisjunction>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}

		/// <summary>
		/// Performs disjunction
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of disjunction</returns>
		public static VariableWrapper operator |(VariableWrapper a, IVariable b)
		{
			return a.Wrapped.Operation<Disjunction>(b).Wrap();
		}

		/// <summary>
		/// Performs disjunction
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of disjunction</returns>
		public static VariableWrapper operator |(VariableWrapper a, VariableWrapper b)
		{
			return a.Wrapped.Operation<Disjunction>(b.Wrapped).Wrap();
		}

		/// <summary>
		/// Performs disjunction
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of disjunction</returns>
		public static VariableWrapper operator |(VariableWrapper a, int b)
		{
			return a.Wrapped.Operation<Disjunction>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}
		
		/// <summary>
		/// Performs conjunction
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of conjunction</returns>
		public static VariableWrapper operator &(VariableWrapper a, IVariable b)
		{
			return a.Wrapped.Operation<Conjunction>(b).Wrap();
		}

		/// <summary>
		/// Performs conjunction
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of conjunction</returns>
		public static VariableWrapper operator &(VariableWrapper a, VariableWrapper b)
		{
			return a.Wrapped.Operation<Conjunction>(b.Wrapped).Wrap();
		}

		/// <summary>
		/// Performs conjunction
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of conjunction</returns>
		public static VariableWrapper operator &(VariableWrapper a, int b)
		{
			return a.Wrapped.Operation<Conjunction>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}
		
		/// <summary>
		/// Performs LT comparison
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of LT comparison</returns>
		public static VariableWrapper operator <(VariableWrapper a, IVariable b)
		{
			return a.Wrapped.Operation<IsLessThan>(b).Wrap();
		}

		/// <summary>
		/// Performs LT comparison
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of LT comparison</returns>
		public static VariableWrapper operator <(VariableWrapper a, VariableWrapper b)
		{
			return a.Wrapped.Operation<IsLessThan>(b.Wrapped).Wrap();
		}

		/// <summary>
		/// Performs LT comparison
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of LT comparison</returns>
		public static VariableWrapper operator <(VariableWrapper a, int b)
		{
			return a.Wrapped.Operation<IsLessThan>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}

		/// <summary>
		/// Performs LT comparison
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of LT comparison</returns>
		public static VariableWrapper operator <(VariableWrapper a, double b)
		{
			return a.Wrapped.Operation<IsLessThan>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}

		/// <summary>
		/// Performs GT comparison
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of GT comparison</returns>
		public static VariableWrapper operator >(VariableWrapper a, IVariable b)
		{
			return a.Wrapped.Operation<IsGreaterThan>(b).Wrap();
		}

		/// <summary>
		/// Performs GT comparison
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of GT comparison</returns>
		public static VariableWrapper operator >(VariableWrapper a, VariableWrapper b)
		{
			return a.Wrapped.Operation<IsGreaterThan>(b.Wrapped).Wrap();
		}

		/// <summary>
		/// Performs GT comparison
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of GT comparison</returns>
		public static VariableWrapper operator >(VariableWrapper a, int b)
		{
			return a.Wrapped.Operation<IsGreaterThan>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}

		/// <summary>
		/// Performs GT comparison
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of GT comparison</returns>
		public static VariableWrapper operator >(VariableWrapper a, double b)
		{
			return a.Wrapped.Operation<IsGreaterThan>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}

		/// <summary>
		/// Performs LE comparison
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of LE comparison</returns>
		public static VariableWrapper operator <=(VariableWrapper a, IVariable b)
		{
			return a.Wrapped.Operation<IsLessOrEqual>(b).Wrap();
		}

		/// <summary>
		/// Performs LE comparison
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of LE comparison</returns>
		public static VariableWrapper operator <=(VariableWrapper a, VariableWrapper b)
		{
			return a.Wrapped.Operation<IsLessOrEqual>(b.Wrapped).Wrap();
		}

		/// <summary>
		/// Performs LE comparison
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of LE comparison</returns>
		public static VariableWrapper operator <=(VariableWrapper a, int b)
		{
			return a.Wrapped.Operation<IsLessOrEqual>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}

		/// <summary>
		/// Performs LE comparison
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of LE comparison</returns>
		public static VariableWrapper operator <=(VariableWrapper a, double b)
		{
			return a.Wrapped.Operation<IsLessOrEqual>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}

		/// <summary>
		/// Performs GE comparison
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of GE comparison</returns>
		public static VariableWrapper operator >=(VariableWrapper a, IVariable b)
		{
			return a.Wrapped.Operation<IsGreaterOrEqual>(b).Wrap();
		}

		/// <summary>
		/// Performs GE comparison
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of GE comparison</returns>
		public static VariableWrapper operator >=(VariableWrapper a, VariableWrapper b)
		{
			return a.Wrapped.Operation<IsGreaterOrEqual>(b.Wrapped).Wrap();
		}

		/// <summary>
		/// Performs GE comparison
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of GE comparison</returns>
		public static VariableWrapper operator >=(VariableWrapper a, int b)
		{
			return a.Wrapped.Operation<IsGreaterOrEqual>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}

		/// <summary>
		/// Performs GE comparison
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of GE comparison</returns>
		public static VariableWrapper operator >=(VariableWrapper a, double b)
		{
			return a.Wrapped.Operation<IsGreaterOrEqual>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}

		/// <summary>
		/// Performs EQ comparison
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of EQ comparison</returns>
		public static VariableWrapper operator ==(VariableWrapper a, IVariable b)
		{
			if ((object) a == null)
			{
				throw new ArgumentException(nameof(a));
			}
			return a.Wrapped.Operation<IsEqual>(b).Wrap();
		}

		/// <summary>
		/// Performs EQ comparison
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of EQ comparison</returns>
		public static VariableWrapper operator ==(VariableWrapper a, VariableWrapper b)
		{
			if ((object) a == null)
			{
				throw new ArgumentException(nameof(a));
			}
			return a.Wrapped.Operation<IsEqual>(b.Wrapped).Wrap();
		}

		/// <summary>
		/// Performs EQ comparison
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of EQ comparison</returns>
		public static VariableWrapper operator ==(VariableWrapper a, int b)
		{
			if ((object) a == null)
			{
				throw new ArgumentException(nameof(a));
			}
			return a.Wrapped.Operation<IsEqual>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}

		/// <summary>
		/// Performs EQ comparison
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of EQ comparison</returns>
		public static VariableWrapper operator ==(VariableWrapper a, double b)
		{
			if ((object) a == null)
			{
				throw new ArgumentException(nameof(a));
			}
			return a.Wrapped.Operation<IsEqual>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}

		/// <summary>
		/// Performs NEQ comparison
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of NEQ comparison</returns>
		public static VariableWrapper operator !=(VariableWrapper a, IVariable b)
		{
			if ((object)a == null)
			{
				throw new ArgumentException(nameof(a));
			}
			return a.Wrapped.Operation<IsNotEqual>(b).Wrap();
		}

		/// <summary>
		/// Performs NEQ comparison
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of NEQ comparison</returns>
		public static VariableWrapper operator !=(VariableWrapper a, VariableWrapper b)
		{
			if ((object)a == null)
			{
				throw new ArgumentException(nameof(a));
			}
			return a.Wrapped.Operation<IsNotEqual>(b.Wrapped).Wrap();
		}

		/// <summary>
		/// Performs NEQ comparison
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of NEQ comparison</returns>
		public static VariableWrapper operator !=(VariableWrapper a, int b)
		{
			if ((object)a == null)
			{
				throw new ArgumentException(nameof(a));
			}
			return a.Wrapped.Operation<IsNotEqual>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}

		/// <summary>
		/// Performs NEQ comparison
		/// </summary>
		/// <param name="a">First argument</param>
		/// <param name="b">Second argument</param>
		/// <returns>Result of NEQ comparison</returns>
		public static VariableWrapper operator !=(VariableWrapper a, double b)
		{
			if ((object)a == null)
			{
				throw new ArgumentException(nameof(a));
			}
			return a.Wrapped.Operation<IsNotEqual>(a.Wrapped.MilpManager.FromConstant(b)).Wrap();
		}

		private bool Equals(VariableWrapper other)
		{
			return Equals(Wrapped, other.Wrapped);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((VariableWrapper)obj);
		}

		public override int GetHashCode()
		{
			return Wrapped?.GetHashCode() ?? 0;
		}

		public IMilpManager MilpManager
		{
			get { return Wrapped.MilpManager; }
			set { Wrapped.MilpManager = value; }
		}

		public Domain Domain
		{
			get { return Wrapped.Domain; }
			set { Wrapped.Domain = value; }
		}

		public string Name
		{
			get { return Wrapped.Name; }
			set { Wrapped.Name = value; }
		}

		public double? ConstantValue
		{
			get { return Wrapped.ConstantValue; }
			set { Wrapped.ConstantValue = value; }
		}

		public string Expression
		{
			get { return Wrapped.Expression; }
			set { Wrapped.Expression = value; }
		}
	}
}