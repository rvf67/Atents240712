using System.Collections;

namespace _01_Console
{
    // C#의 메모리구조
    // 구분 : Heap 메모리, Stack 메모리
    // Heap : 운영체제가 관리하는 메모리, 크기가 매우 크다. 속도가 느리다. class가 저장됨, 참조타입
    // Stack : 프로그램이 실행되었을 때 이미 할당 받은 메모리. 크기가 작다. 속도가 빠르다. struct(구조체)가 저장됨, 값타입

    // 복사 방식 : 깊은복사(완전한 사본을 만드는 것), 얕은복사(주소만 넘겨주는 것)

    // 참조타입(Reference type) : 얕은 복사. 복사속도가 빠르다. 실체는 1개만 있다.
    // 값타입(Value type) : 깊은 복사. 복사속도는 느리다. 실체가 복사한만큼 생긴다.


    // static : 정적. 프로그램이 실행되기 전에 결정되어 있는 것
    // dynamic : 동적. 프로그램 실행 중에 결정되는 것


    public class TestClass
    {
        // 접근제한자(Access Modifier)
        private int value1;     // private : 나만 사용할 수 있다.
        protected int value2;   // protected : 나와 나를 상속받은 곳에서만 사용할 수 있다.
        public int value3;      // public : 모두가 사용할 수 있다.

        void Test()
        {
            int a = 10;
            int b = a;

            b = 20;
            //ArrayList array = new ArrayList();
            //array.Add(10);
            //array.Add(10.5f);
            //array.Add("Hello");
        }
    }

    public class TestChildClass : TestClass
    {
        void Test2()
        {
            TestClass a = new TestClass();
            a.value3 = 10;
            TestClass b = a;
            b.value3 = 20;
        }
    }

    internal class Program
    {
        enum AgeCategory
        {
            Child = 0,
            Elemetry,
            Middle,
            High,
            Adult
        }

        enum PointGrade
        {
            A,
            B,
            C,
            D,
            F
        }

        int Test(int a, int b, float c)
        {
            int result = 10;
            return result;
        }

        // 함수 이름: 다른 함수와 구분하기 위한 것(사람용)
        // 파라메터 : 함수를 실행하는데 필요한 데이터(0개 이상)
        // 리턴타입 : 함수가 종료되었을 때 돌려주는 데이터 타입(void는 리턴값이 없다는 의미)
        // 함수 바디 : 함수가 실행될 때의 실제 코드

        static void PrintMyData(string name, string age, string address)
        {
            Console.WriteLine($"저는 {address}에 사는 {name}({age})입니다.");
        }

        static void GuGuDan(int dan)
        {
            Console.WriteLine($"{dan}단을 출력합니다.");
            for (int i = 1; i < 10; i++)
            {
                Console.WriteLine($"{dan} * {i} = {dan * i}");
            }
        }

        static void Main(string[] args)
        {
            // 7/16--------------------------------------------------------------------------------------

            // 반복문
            //int count = 0;
            //while(count < 3)        // while : ()사이의 조건이 참이면 계속 반복해서 {}안의 내용을 실행
            //{
            //    Console.WriteLine("Hello");
            //    count++;
            //}

            //Console.WriteLine("-----------------------");
            //count = 0;
            //do
            //{
            //    Console.WriteLine("Hello");
            //    count++;
            //} while (count < 3);    // do-while : 우선 {} 사이의 코드를 실행하고 ()사이의 조건이 참이면 {}를 반복 실행한다.

            //Console.WriteLine("-----------------------");
            //for(int i=0;i<3;i++)    // for(초기화;조건부;증감부)
            //{
            //    Console.WriteLine("Hello");
            //}

            //Console.WriteLine("-----------------------");
            //// 배열(Array) : 같은 종류의 데이터타입을 가지는 값들을 하나로 묶어놓은 것
            //int[] intArray;
            //intArray = new int[3];
            //intArray[0] = 1;
            //intArray[1] = 2;
            //intArray[2] = 3;
            ////intArray[3] = 4;  // index out of range exception!!!!

            //foreach(int i in intArray)
            //{
            //    Console.WriteLine($"Hello - {i}");
            //}

            // 구구단 출력하기
            // 1. 숫자입력받기(숫자 변경 실패하면 다시 받기)
            // 2. 입력 받은 숫자의 구구단 출력하기
            // 3. 파라메터로 받은 숫자의 구구단 출력하는 함수 만들기

            bool isSuccess = false;
            int result = 0;
            do
            {
                Console.Write("단수를 입력하세요 : ");
                isSuccess = int.TryParse(Console.ReadLine(), out result);
            } while (!isSuccess);

            Console.WriteLine($"{result}단을 출력합니다.");
            for(int i = 1 ; i<10 ; i++)
            {
                Console.WriteLine($"{result} * {i} = {result * i}");
            }

            GuGuDan(result);

            TestClass aaa = new TestClass();
            aaa.value3 = 10;
            TestClass bbb = new TestClass();

            // 7/15--------------------------------------------------------------------------------------
            //Console.WriteLine("저는 고병조입니다. 나이는 43살입니다.");
            //int age = 43;
            //Console.WriteLine("저는 고병조입니다. 나이는 " + age + "살입니다.");   // 절대 비추천
            //// 합칠 때는 이런 방식으로 처리. 쌍따옴표앞에 $붙이고 변수는 {} 사이에 넣기
            //Console.WriteLine($"저는 고병조입니다. 나이는 {age}살입니다.");        

            //string test1 = "테스트";
            //string test2 = "22222";
            //string test3 = test1 + test2;   // 테스트22222
            //Console.WriteLine(test3);
            //test3 = "Hello Hello";
            //Console.WriteLine(test3);

            //// 값타입(Value type) : 스택 메모리에 저장, int, float, bool, 기타 구조체
            //// 참조타입(Reference type) : 힙 메모리에 저장, string, 기타 클래스

            //// null : 비어있다라는 것을 표시하는 단어. 기본적으로 참조타입만 가능
            //// nullable type: 널 가능한 타입. 값타입에 붙여서 사용 가능

            //int? a; // a는 null
            //a = 10; // a는 10

            //string? result = Console.ReadLine();
            //Console.WriteLine(result);

            //// 실습
            //// 1. 이름 입력받기 ( "이름을 입력하세요 : " 라고 출력하고 입력 받기)
            //// 2. 나이 입력받기 ( "나이를 입력하세요 : " 라고 출력하고 입력 받기)
            //// 3. 주소 입력받기 ( "사는 곳을 입력하세요 : " 라고 출력하고 입력 받기)
            //// 4. 이름, 나이, 주소를 한번에 출력하기

            //Console.Write("이름을 입력하세요 : ");
            //string? name = Console.ReadLine();
            //Console.Write("나이를 입력하세요 : ");
            //string? ageString = Console.ReadLine();
            ////int age = int.Parse(ageString);
            //Console.Write("사는 곳을 입력하세요 : ");
            //string? address = Console.ReadLine();

            //PrintMyData(name, ageString, address);
            ////Console.WriteLine($"저는 {address}에 사는 {name}({ageString})입니다.");

            // 제어문(Control Statement) - 조건문(if, if-else, switch), 반복문

            //int age;
            //Console.Write("나이를 입력해 주세요 : ");
            //string ageString = Console.ReadLine();
            //age = int.Parse(ageString);

            //// if : () 사이에 있는 조건이 true면 {} 사이에 있는 코드를 실행한다.
            //if(age > 20)
            //{
            //    Console.WriteLine("성인입니다.");
            //}

            ////if(age <= 20) // 비추천. 두번 확인함.
            //if (age < 21)
            //{
            //    Console.WriteLine("미성년자입니다.");
            //}

            //// if-else : () 사이에 있는 조건이 true면 if아래에 있는 {} 사이의 코드를 실행, false면 else 아래에 있는 {} 사이의 코드를 실행
            //if(age > 20)
            //{
            //    Console.WriteLine("성인입니다.");
            //}
            //else
            //{
            //    Console.WriteLine("미성년자입니다.");
            //}

            // 실습
            // 1. 나이를 입력받기
            // 2. 8살 미만이면 "미취학 아동입니다" 출력
            // 3. 13살 미만이면 "초등학생입니다" 출력
            // 4. 16살 미만이면 "중학생입니다" 출력
            // 5. 19살 미만이면 "고등학생입니다" 출력

            //int age = 0;
            //Console.Write("나이를 입력하세요 : ");
            //string ageString = Console.ReadLine();
            //age = int.Parse(ageString);
            //int categoty = 0;       // 매직넘버 : 안쓰는게 좋다.
            //AgeCategory ageCategory;

            //if (age < 8)
            //{
            //    Console.WriteLine("미취학 아동입니다.");
            //    categoty = 0;
            //    ageCategory = AgeCategory.Child;
            //}
            //else if(age < 13)
            //{
            //    Console.WriteLine("초등학생입니다.");
            //    categoty = 1;
            //    ageCategory = AgeCategory.Elemetry;
            //}
            //else if (age < 16)
            //{
            //    Console.WriteLine("중학생입니다.");
            //    categoty = 2;
            //    ageCategory = AgeCategory.Middle;
            //}
            //else if (age < 19)
            //{
            //    Console.WriteLine("고등학생입니다.");
            //    categoty = 3;
            //    ageCategory = AgeCategory.High;
            //}
            //else
            //{
            //    Console.WriteLine("성인입니다.");
            //    categoty = 4;
            //    ageCategory = AgeCategory.Adult;
            //}

            //// switch : () 사이에 있는 값에 따라 다른 코드를 수행하는 조건문
            ////switch(categoty)
            ////{
            ////    case 0:
            ////        Console.WriteLine("미취학 아동은 1000원입니다.");
            ////        break;
            ////    case 1:
            ////        Console.WriteLine("초등학생은 2000원입니다.");
            ////        break;
            ////    case 2:
            ////        Console.WriteLine("중학생은 3000원입니다.");
            ////        break;
            ////    case 3:
            ////        Console.WriteLine("고등학생은 5000원입니다.");
            ////        break;
            ////    case 4:
            ////        Console.WriteLine("성인은 10000원입니다.");
            ////        break;
            ////}

            //switch (ageCategory)
            //{
            //    case AgeCategory.Child:
            //        Console.WriteLine("미취학 아동은 1000원입니다.");
            //        break;
            //    case AgeCategory.Elemetry:
            //        Console.WriteLine("초등학생은 1000원입니다.");
            //        break;
            //    case AgeCategory.Middle:
            //        Console.WriteLine("중학생은 3000원입니다.");
            //        break;
            //    case AgeCategory.High:
            //        Console.WriteLine("고등학생은 5000원입니다.");
            //        break;
            //    case AgeCategory.Adult:
            //        Console.WriteLine("성인은 10000원입니다.");
            //        break;
            //    default:
            //        break;
            //}

            ////int point = int.Parse(Console.ReadLine());
            ////int point;
            //PointGrade pointGrade = PointGrade.F;
            //Console.Write("점수를 입력해 주세요 : ");
            //if( int.TryParse(Console.ReadLine(), out int point) )
            //{
            //    if( point > 89 )
            //    {
            //        pointGrade = PointGrade.A;
            //    }
            //    else if( point > 79 )
            //    {
            //        pointGrade = PointGrade.B;
            //    }
            //    else if ( point > 69 )
            //    {
            //        pointGrade = PointGrade.C;
            //    }
            //    else if( point > 59 )
            //    {
            //        pointGrade = PointGrade.D;
            //    }

            //    //switch (pointGrade)
            //    //{
            //    //    case PointGrade.A:
            //    //        break;
            //    //    case PointGrade.B:
            //    //        break;
            //    //    case PointGrade.C:
            //    //        break;
            //    //    case PointGrade.D:
            //    //        break;
            //    //    case PointGrade.F:
            //    //        break;
            //    //}

            //    Console.WriteLine($"당신의 성적은 {pointGrade}입니다.");
            //}
            //else
            //{
            //    Console.WriteLine("변환 실패");
            //}

            //// 실습
            //// 1. 성적용 enum 만들기(A,B,C,D,F)
            //// 2. 점수를 입력 받아서 90점 이상이면 A, 80 이상이면 B, 70점 이상이면 C, 60점 이상이면 D, 60점 미만이면 F를 주기

            // 연산자(operator)
            // 산술연산자 : + - * / %, 산수 계산하는데 사용되는 연산자들
            // 대입연산자 : =, = 오른쪽에 있는 값을 왼쪽 변수에 대입하는 연산자
            // 비교연산자 : <, >, <=, >=, ==(같다), !=(다르다), 항상 결과는 true 아니면 false
            //             !!!!!절대로 float같은 실수 타입은 ==로 비교해서는 안된다.
            // 논리연산자 : &&(and, 연산자의 오른쪽과 왼쪽이 모두 true일 때만 true), ||(or, 둘중 하나만 true면 true), 항상 결과는 true아니면 false. 
            //              true  && true;  // true
            //              true  && false; // false 
            //              false && true;  // false
            //              false && false; // false
            //              true  || true;  // true
            //              true  || false; // true
            //              false || true;  // true
            //              false || false; // false
            // 비트연산자 : &(and, 오른쪽에 있는 비트와 왼쪽에 있는 비트가 모두 1일때 1), |(or, 오른쪽에 있는 비트와 왼쪽에 있는 비트가 하나만 1이면 1)
            //              int a = 123;
            //              a = 0b_0111_1011;   // 이진수로 쓴 123
            //              int b1 = 0b_1010;
            //              int b2 = 0b_1100;
            //              int c1 = b1 & b2;   // 0b_1000
            //              int c2 = b1 | b2;   // 0b_1110
            // 증감연산자 : ++, --, +=, -=, *=, /=
            //              a++; a = a + 1; 
            //              a--; a = a -1;
            //              a += 10; a = a + 10;


            //// 7/12--------------------------------------------------------------------------------------
            //// Comment : 주석. 코드에 아무런 영향을 주지 않는다. 코드 설명용

            ///*
            // * 여러줄 주석 처리하기
            // */

            ///// 엔터치면 아래줄로 주석으로 만듬
            ///// 

            //// 디버깅용 단축키
            //// F5 : 디버그 모드로 시작. 디버깅 중일 때는 다음 브레이크 포인트까지 진행
            //// F9 : 브레이크 포인트 지정
            //// F10 : 현재 멈춰있는 지점에서 다음 점으로 넘어가기

            //// 편집용 단축키
            //// Ctrl + D : 현재 코드를 한줄 복사해서 붙여넣기
            //// Shift + Del : 현재 줄 지우기
            //// Ctrl + 좌우화살표 : 단어 단위로 이동하기
            //// Ctrl + 위아래화살표 : 커서 위치는 그대로 두고 페이지를 위아래로 움직이기

            //Console.WriteLine("Hello, World! - 고병조");
            //Console.WriteLine("Hello, World 222222222222222222");
            //Console.WriteLine("가가가가");

            //// 변수 : 데이터를 저장해 놓은 곳(메모리에서의 위치)
            //// 함수 : 특정한 기능을 수행하는 코드 덩어리
            //// 클래스 : 특정한 동작을 하는 물체를 표현하기 위해 변수와 함수를 모아 놓은 것

            //// 데이터 타입 : 변수의 종류.
            ////  정수(Integer) : int, 소수점이 없는 숫자(0, 10, -25 등등)
            ////  실수(float) : float, 소수점이 있는 숫자(3.14, 5.2222 등등)
            ////  불리언(boolean) : bool, true나 false만 저장하는 데이터타입
            ////  문자열(string) : string, 글자 여러개를 저장하는 데이터 타입

            //int a = 10; // integer타입으로 변수를 만들고 이름을 a라고 붙이고 a에 10이라는 값을 넣어라
            //a = 2100000000;
            ////a = 4100000000;   // 사이즈를 넘어가면 실행안됨
            ////a = 21.5f;        // 데이터 타입이 다르면 안됨

            //float b = 20.123456111111111111111111111111111111111f;
            //double b2 = 20.123456789123456789123456789;
            //b = 20; // int의 표현범위가 float보다 작기 때문에 가능

            //bool c = true;      // (11 > 5) == true,  (11 < 5) == false, 참 또는 거짓을 저장
            //string d = "hello"; // 문자열, 문자가 여러개 있다.(직접 비교는 최대한 피해야 한다.)
            ////104 101 108 108 111 0

        }
    }
}
