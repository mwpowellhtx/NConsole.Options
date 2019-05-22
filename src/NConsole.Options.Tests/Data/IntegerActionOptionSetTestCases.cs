//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace NConsole.Options
//{
//    using static TestFixtureBase;

//    internal class IntegerActionOptionSetTestCases : TypedAlphabetSoupTestCasesBase<int>
//    {
//        protected override string DefaultValueString => "0";

//        protected override IEnumerable<int> TypedValues
//        {
//            get
//            {
//                int Calculate(int y) => y * (int) Math.Pow(10, y);
//                return GetRange(1, 2, 3, 4, 5).Select(Calculate);
//            }
//        }

//        protected override string RenderTypedValue(int value) => $"{value}".ToLower();

//        private static IEnumerable<object[]> _privateCases;

//        protected override IEnumerable<object[]> Cases => _privateCases ?? (_privateCases = base.Cases);
//    }
//}
