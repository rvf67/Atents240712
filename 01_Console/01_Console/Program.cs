namespace _01_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {          
            // Comment : 주석. 코드에 아무런 영향을 주지 않는다. 코드 설명용

            /*
             * 여러줄 주석 처리하기
             */

            /// 엔터치면 아래줄로 주석으로 만듬
            /// 

            // 디버깅용 단축키
            // F5 : 디버그 모드로 시작. 디버깅 중일 때는 다음 브레이크 포인트까지 진행
            // F9 : 브레이크 포인트 지정
            // F10 : 현재 멈춰있는 지점에서 다음 점으로 넘어가기

            // 편집용 단축키
            // Ctrl + D : 현재 코드를 한줄 복사해서 붙여넣기
            // Shift + Del : 현재 줄 지우기
            // Ctrl + 좌우화살표 : 단어 단위로 이동하기
            // Ctrl + 위아래화살표 : 커서 위치는 그대로 두고 페이지를 위아래로 움직이기

            Console.WriteLine("Hello, World! - 고병조");
            Console.WriteLine("Hello, World 222222222222222222");
            Console.WriteLine("가가가가");

            // 변수 : 데이터를 저장해 놓은 곳(메모리에서의 위치)
            // 함수 : 특정한 기능을 수행하는 코드 덩어리
            // 클래스 : 특정한 동작을 하는 물체를 표현하기 위해 변수와 함수를 모아 놓은 것

            // 데이터 타입 : 변수의 종류.
            //  정수(Integer) : int, 소수점이 없는 숫자(0, 10, -25 등등)
            //  실수(float) : float, 소수점이 있는 숫자(3.14, 5.2222 등등)
            //  불리언(boolean) : bool, true나 false만 저장하는 데이터타입
            //  문자열(string) : string, 글자 여러개를 저장하는 데이터 타입

            int a = 10; // integer타입으로 변수를 만들고 이름을 a라고 붙이고 a에 10이라는 값을 넣어라
            a = 2100000000;
            //a = 4100000000;   // 사이즈를 넘어가면 실행안됨
            //a = 21.5f;        // 데이터 타입이 다르면 안됨

            float b = 20.5f;
            b = 20; // int의 표현범위가 float보다 작기 때문에 가능

            bool c = true;
            string d = "hello";
        }
    }
}
