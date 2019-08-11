using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp0
{
    /// <summary>
    /// Працівник
    /// </summary>
    class Employee
    {
        // назви введені кирилицею для виведення на українській мові всього тексту
        /// <summary>
        /// Посада в компанії
        /// </summary>
        public enum PositionInCompany
        {
            Продавець,
            Менеджер,
            Директор
        }
        /// <summary>
        /// Місце роботи
        /// </summary>
        public enum PlaceOfWork
        {
            Київ,
            Харків,
            Одеса
        }
        /// <summary>
        /// випадкові числа
        /// </summary>
        private Random rnd = new Random();

        /// <summary>
        /// Ім'я
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Прізвище
        /// </summary>
        public string SecondName { get; set; }
        /// <summary>
        /// Посада в компанії
        /// </summary>
        public PositionInCompany Position { get; set; }
        /// <summary>
        /// Заробітня платня (задається автоматично, але можна міняти)
        /// </summary>
        public int Salary { get; set; }
        /// <summary>
        /// Місце роботи
        /// </summary>
        public PlaceOfWork Place { get; set; }
        /// <summary>
        /// Рік народження
        /// </summary>
        public int YearBirthday { get; set; }
        /// <summary>
        /// Вік
        /// </summary>
        public int Age
            => Math.Abs(DateTime.Now.Year - YearBirthday);

        /// <summary>
        /// Автоматичний вибір ЗП в залежності від посади
        /// </summary>
        /// <param name="position">Посада</param>
        /// <returns></returns>
        public void GetSalary()
        {
            int res = default;

            switch (Position)
            {
                case Employee.PositionInCompany.Продавець:
                    res = rnd.Next(5_000, 10_000);
                    break;
                case Employee.PositionInCompany.Менеджер:
                    res = rnd.Next(10_000, 15_000);
                    break;
                case Employee.PositionInCompany.Директор:
                    res = rnd.Next(15_000, 20_000);
                    break;
            }

            Salary = res;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("\t")
                .Append(SecondName + " ")
                .Append(FirstName + ", ")
                .Append(Age + " років, ")
                .Append($"ЗП: {Salary:N0} грн")
                .ToString();
        }

        public string ToStringFull()
        {
            return new StringBuilder()
                .Append("\t")
                .Append(SecondName + " ")
                .Append(FirstName + ", ")
                .Append(Age + " років, ")
                .Append(Place + ", ")
                .Append(Position + " із ")
                .Append($"ЗП в {Salary:N0} грн")
                .ToString();
        }

    }
}
