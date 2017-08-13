using System.Collections.Generic;
using System.Linq;
using Castle.DynamicProxy;
using MilpManager.Abstraction;
using MilpManager.Implementation;

namespace MilpOperatorsWrapper
{
	public static class VariableWrapperExtensions
	{
		/// <summary>
		/// Wraps variable into wrapper with overloaded operators
		/// </summary>
		/// <param name="variable">Variable to wrap</param>
		/// <returns>Wrapped variable</returns>
		public static VariableWrapper Wrap(this IVariable variable)
		{
			return variable as VariableWrapper ?? WrapInternal(variable);
		}

		private static VariableWrapper WrapInternal(IVariable variable)
		{
			ProxyGenerator generator = new ProxyGenerator();

			var interfacesToProxy = variable
				.GetType()
				.GetInterfaces()
				.Where(t => t != typeof(IVariable))
				.ToArray();

			if (interfacesToProxy.Length == 0)
			{
				return new VariableWrapper(variable);
			}

			var options = new ProxyGenerationOptions();
			options.AddMixinInstance(variable);
			return (VariableWrapper)generator.CreateClassProxy(typeof (VariableWrapper), interfacesToProxy, options, new object[] {variable});
		}


		/// <summary>
		/// Materializes variable in a solver with given name
		/// </summary>
		/// <param name="variable">Variable to materialize</param>
		/// <param name="name">Name of materialized variable</param>
		/// <returns>Variable representing materialized variable</returns>
		public static VariableWrapper Create(this VariableWrapper variable, string name)
		{
			return ((IVariable)variable).Create(name).Wrap();
		}

		/// <summary>
		/// Materializes variable in a solver
		/// </summary>
		/// <param name="variable">Variable to materialize</param>
		/// <returns>Variable representing materialized variable</returns>
		public static VariableWrapper Create(this VariableWrapper variable)
		{
			return ((IVariable)variable).Create().Wrap();
		}

		/// <summary>
		/// Performs operation
		/// </summary>
		/// <typeparam name="TOperationType">Operation type</typeparam>
		/// <param name="variable">Variable to perform operation on</param>
		/// <param name="variables">Operation arguments</param>
		/// <returns>Operation result</returns>
		public static VariableWrapper Operation<TOperationType>(this VariableWrapper variable, params IVariable[] variables) where TOperationType : OperationType
		{
			return ((IVariable)variable).Operation<TOperationType>(variables).Wrap();
		}

		/// <summary>
		/// Performs composite operation
		/// </summary>
		/// <typeparam name="TCompositeOperationType">Operation type</typeparam>
		/// <param name="variable">Variable to perform operation on</param>
		/// <param name="variables">Operation arguments</param>
		/// <returns>Operation result</returns>
		public static IEnumerable<VariableWrapper> CompositeOperation<TCompositeOperationType>(this VariableWrapper variable, params IVariable[] variables) where TCompositeOperationType : CompositeOperationType
		{
			return ((IVariable)variable).CompositeOperation<TCompositeOperationType>(variables).Select(Wrap);
		}

		/// <summary>
		/// Adds constraint to a solver
		/// </summary>
		/// <typeparam name="TConstraintType">Constraint type</typeparam>
		/// <param name="variable">Variable to constrain</param>
		/// <param name="right">Right hand side of a constraint</param>
		/// <returns>Variable passed as an argument</returns>
		public static VariableWrapper Set<TConstraintType>(this VariableWrapper variable, IVariable right) where TConstraintType : ConstraintType
		{
			return ((IVariable) variable).Set<TConstraintType>(right).Wrap();
		}

		/// <summary>
		/// Adds composite constraint to a solver
		/// <param name="variable">Variable to constraint</param>
		/// </summary>
		/// <typeparam name="TCompositeConstraintType">Constraint type</typeparam>
		/// <param name="variable">Variable to constrain</param>
		/// <param name="parameters">Additional constraint parameters</param>
		/// <param name="right">Right hand side of a constraint</param>
		/// <returns>Variable passed as an argument</returns>
		public static VariableWrapper Set<TCompositeConstraintType>(this VariableWrapper variable, ICompositeConstraintParameters parameters, params IVariable[] right) where TCompositeConstraintType : CompositeConstraintType
		{
			return ((IVariable) variable).Set<TCompositeConstraintType>(parameters, right).Wrap();
		}

		/// <summary>
		/// Makes goal
		/// </summary>
		/// <typeparam name="TGoalType">Goal type</typeparam>
		/// <param name="variable">Variable to make goal</param>
		/// <param name="variables">Additional variables required to make a goal</param>
		/// <returns>Variable representing goal</returns>
		public static VariableWrapper MakeGoal<TGoalType>(this VariableWrapper variable, params IVariable[] variables) where TGoalType : GoalType
		{
			return ((IVariable) variable).MakeGoal<TGoalType>(variables).Wrap();
		}

		/// <summary>
		/// Creates new variable equal to passed variable and with adjusted domain
		/// </summary>
		/// <param name="variable">Original variable</param>
		/// <param name="newDomain">Domain of a new variable</param>
		/// <returns>Variable with modified domain</returns>
		public static VariableWrapper ChangeDomain(this VariableWrapper variable, Domain newDomain)
		{
			return ((IVariable) variable).ChangeDomain(newDomain).Wrap();
		}
	}
}