//using System;
//using System.Collections.Generic;

//namespace NConsole.Options
//{
//    internal class StringActionOptionSetTestCases : TypedAlphabetSoupTestCasesBase<Guid>
//    {
//        protected override string DefaultValueString => string.Empty;

//        protected override IEnumerable<Guid> TypedValues
//        {
//            get { yield return Guid.NewGuid(); }
//        }

//        protected override string RenderTypedValue(Guid value) => $"{value:N}";

//        private static IEnumerable<object[]> _privateCases;

//        protected override IEnumerable<object[]> Cases => _privateCases ?? (_privateCases = base.Cases);
//    }
//}
