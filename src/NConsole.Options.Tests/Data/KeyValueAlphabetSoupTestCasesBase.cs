//using System.Collections.Generic;
//using System.Linq;

//namespace NConsole.Options
//{
//    using Data;
//    using static Characters;

//    internal abstract class KeyValueAlphabetSoupTestCasesBase<TKey, TValue> : AlphabetSoupTestCasesBase
//    {
//        private IEnumerable<object[]> _privateCases;

//        protected override IEnumerable<object[]> Cases
//        {
//            get
//            {
//                IEnumerable<object[]> GetAll(char delimiter)
//                {
//                    yield break;
//                }

//                return _privateCases ?? (_privateCases = MergeCases(
//                           AdaptBaseCases(Equal).Concat(AdaptBaseCases(Colon)).ToArray()
//                           , GetAll(Equal).Concat(GetAll(Colon)).ToArray()
//                       ));
//            }
//        }
//    }
//}
