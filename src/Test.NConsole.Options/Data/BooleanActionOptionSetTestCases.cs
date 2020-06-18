//using System.Collections.Generic;

//namespace NConsole.Options
//{
//    internal class BooleanActionOptionSetTestCases : TypedAlphabetSoupTestCasesBase<bool>
//    {
//        protected override string DefaultValueString => "false";

//        protected override IEnumerable<bool> TypedValues
//        {
//            get
//            {
//                yield return false;
//                yield return true;
//            }
//        }

//        protected override string RenderTypedValue(bool value) => $"{value}".ToLower();

//        private static IEnumerable<object[]> _privateCases;

//        protected override IEnumerable<object[]> Cases => _privateCases ?? (_privateCases = base.Cases);
//    }
//}
