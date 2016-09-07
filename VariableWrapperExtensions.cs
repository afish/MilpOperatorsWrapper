using MilpManager.Abstraction;

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
            return new VariableWrapper(variable);
        }
    }
}