using System;

namespace Utils {
    public class Either<T1, T2> {
        readonly T1 _variant1;
        readonly T2 _variant2;
        readonly bool isFirst;

        public Either(T1 value) {
            _variant1 = value;
            isFirst = true;
        }

        public Either(T2 value) {
            _variant2 = value;
            isFirst = false;
        }

        public void Get(Action<T1> handler1, Action<T2> handler2) {
            if (isFirst) {
                handler1(_variant1);
            } else {
                handler2(_variant2);
            }
        }
    }
}
