using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp0
{
    class Program
    {
        /// <summary>
        /// Набір імен
        /// https://uk.wikipedia.org/wiki/Українські_імена
        /// </summary>
        enum FirstName
        {
            Андрій,
            Антоній,
            Артем,
            Богдан,
            Борис,
            Вадим,
            Валерій,
            Варфоломій,
            Василь,
            Віктор,
            Владислав,
            Володимир,
            Георгій,
            Дмитро
        }
        /// <summary>
        /// Набір прізвищ
        /// https://uk.wikipedia.org/wiki/Українські_прізвища
        /// </summary>
        enum SecondName
        {
            Мельник,
            Ковальчук,
            Шевченко,
            Пінчук,
            Попович,
            Іванов,
            Бойко,
            Коваленко,
            Ткачук,
            Волошин,
            Козак,
            Поліщук,
            Бондаренко,
            Павлюк
        }
        /// <summary>
        /// випадкові числа
        /// </summary>
        private static Random rnd = new Random();

        static void Main()
        {
            // Join Unicode
            Console.OutputEncoding = Encoding.Unicode;

            #region Введення даних
            // кількість робітників
            int empCount = rnd.Next(10, 28);

            // масиви перерахунків посад і місця роботи
            var position = Enum.GetValues(typeof(Employee.PositionInCompany))
                .Cast<Employee.PositionInCompany>().ToArray();
            var place = Enum.GetValues(typeof(Employee.PlaceOfWork))
                .Cast<Employee.PlaceOfWork>().ToArray();
            // імена і прізвища
            var names = Enum.GetValues(typeof(FirstName))
                .Cast<FirstName>().ToArray();
            var surnames = Enum.GetValues(typeof(SecondName))
                .Cast<SecondName>().ToArray();

            // створення колекції робітників
            List<Employee> employees = new List<Employee>();

            for (int i = 0; i < empCount; i++)
            {
                employees.Add(new Employee()
                {
                    FirstName = names[rnd.Next(0, names.Length)].ToString(),
                    SecondName = surnames[rnd.Next(0, surnames.Length)].ToString(),
                    Place = place[rnd.Next(0, place.Length)],
                    Position = position[rnd.Next(0, position.Length)],
                    YearBirthday = rnd.Next(1970, 2000),
                });
                // так як там отримується рандомна позиція, а доступ отримати
                // зразу неможливо, тому введено автоматичне присвоєння, яку можна по потребі знімнити
                employees[i].GetSalary();
            }
            #endregion

#if true
            // тестування, виведення всіх робітників
            Show($"\n\tВсі робітники, {employees.Count} чоловік:\n");
            foreach (var i in employees)
            {
                Console.WriteLine(i.ToStringFull());
            }
#endif

            // запити
            #region За допомогою методів розширення
#if false
            // вік більше 25
            var age = employees.Where(t => t.Age > 25).Select(t => t);
            // ЗП більше 10_000
            var salary = employees.Where(t => t.Salary > 10_000).Select(t => t);
            // проживає в Києві
            var town = employees.Where(t => t.Place == Employee.PlaceOfWork.Київ).Select(t => t);
            // всі фільтри одночасно
            var allFilter = employees
                .Where(t => t.Age > 25)
                .Where(t => t.Salary > 10_000)
                .Where(t => t.Place == Employee.PlaceOfWork.Київ)
                .Select(t => t);
#endif
            #endregion

            #region За допомогою Linq (SQL синтаксис)
#if false
            // вік більше 25
            var age = from i in employees
                      where i.Age > 25
                      select i;
            // ЗП більше 10_000
            var salary = from i in employees
                         where i.Salary > 10_000
                         select i;
            // проживає в Києві
            var town = from i in employees
                       where i.Place == Employee.PlaceOfWork.Київ
                       select i;
            // всі фільтри одночасно
            var allFilter = from i in employees
                            where i.Age > 25
                            where i.Salary > 10_000
                            where i.Place == Employee.PlaceOfWork.Київ
                            select i;
#endif
            #endregion

            #region Статичними методами
#if true
            // вік більше 25
            var age = Enumerable.Select(
                Enumerable.Where(employees, i => i.Age > 25),
                i => i);
            // ЗП більше 10_000
            var salary = Enumerable.Select(
                Enumerable.Where(employees, i => i.Salary > 10_000),
                i => i);
            // проживає в Києві
            var town = Enumerable.Select(
                Enumerable.Where(employees, i => i.Place == Employee.PlaceOfWork.Київ),
                i => i);
            // всі фільтри одночасно
            var allFilter = Enumerable.Select(
                Enumerable.Where(
                    Enumerable.Where(
                        Enumerable.Where(employees, i => i.Age > 25),
                    i => i.Salary > 10_000),
                i => i.Place == Employee.PlaceOfWork.Київ),
                i => i); 
#endif
            #endregion

            #region Виведення інформації
            Show($"\n\tРобітники старші 25 років:\n");
            foreach (var i in age)
            {
                Console.WriteLine(i.ToString());
            }

            Show($"\n\tРобітники із ЗП більше 10 000 грн:\n");
            foreach (var i in salary)
            {
                Console.WriteLine(i.ToString());
            }

            Show($"\n\tРобітники, які проживають в Києві:\n");
            foreach (var i in salary)
            {
                Console.WriteLine(i.ToString());
            }

            Show($"\n\tЗастосування всіх фільтрів:\n");
            foreach (var i in allFilter)
            {
                Console.WriteLine(i.ToString());
            }
            #endregion

            // repeat
            DoExitOrRepeat();
        }

        /// <summary>
        /// Відображення заголовку зеленим кольором
        /// </summary>
        /// <param name="s"></param>
        private static void Show(string s)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(s);
            Console.ResetColor();
        }

        /// <summary>
        /// Метод виходу або повторення методу Main()
        /// </summary>
        static void DoExitOrRepeat()
        {
            Console.WriteLine("\n\nСпробувати ще раз: [т, н]");
            Console.Write("\t");
            var button = Console.ReadKey(true);

            if ((button.KeyChar.ToString().ToLower() == "т") ||
                (button.KeyChar.ToString().ToLower() == "n")) // можливо забули переключити розкладку клавіатури
            {
                Console.Clear();
                Main();
                // без використання рекурсії
                //Process.Start(Assembly.GetExecutingAssembly().Location);
                //Environment.Exit(0);
            }
            else
            {
                // закриває консоль
                Environment.Exit(0);
            }
        }
    }
}
