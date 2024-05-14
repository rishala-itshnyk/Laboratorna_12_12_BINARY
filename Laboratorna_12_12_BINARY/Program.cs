using System;
using System.Collections.Generic;

public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Exam1 { get; set; }
    public int Exam2 { get; set; }
    public int Exam3 { get; set; }
}

public class TreeNode
{
    public Student Student { get; set; }
    public TreeNode Left { get; set; }
    public TreeNode Right { get; set; }
}

public class BinaryTree
{
    private TreeNode root;

    public void AddStudent(Student student)
    {
        root = Insert(root, student);
    }

    private TreeNode Insert(TreeNode node, Student student)
    {
        if (node == null)
        {
            node = new TreeNode { Student = student };
        }
        else if (String.Compare(student.LastName, node.Student.LastName) < 0)
        {
            node.Left = Insert(node.Left, student);
        }
        else
        {
            node.Right = Insert(node.Right, student);
        }
        return node;
    }

    public bool RemoveStudent(string firstName, string lastName)
    {
        TreeNode parent = null;
        TreeNode current = root;

        while (current != null)
        {
            int compareResult = String.Compare(lastName, current.Student.LastName);
            if (compareResult == 0)
            {
                if (current.Student.FirstName == firstName)
                {
                    if (current.Left == null && current.Right == null)
                    {
                        if (parent == null)
                        {
                            root = null;
                        }
                        else if (String.Compare(lastName, parent.Student.LastName) < 0)
                        {
                            parent.Left = null;
                        }
                        else
                        {
                            parent.Right = null;
                        }
                    }
                    else if (current.Left == null)
                    {
                        if (parent == null)
                        {
                            root = current.Right;
                        }
                        else if (String.Compare(lastName, parent.Student.LastName) < 0)
                        {
                            parent.Left = current.Right;
                        }
                        else
                        {
                            parent.Right = current.Right;
                        }
                    }
                    else if (current.Right == null)
                    {
                        if (parent == null)
                        {
                            root = current.Left;
                        }
                        else if (String.Compare(lastName, parent.Student.LastName) < 0)
                        {
                            parent.Left = current.Left;
                        }
                        else
                        {
                            parent.Right = current.Left;
                        }
                    }
                    else
                    {
                        TreeNode successor = FindMin(current.Right);
                        current.Student = successor.Student;
                        RemoveStudent(successor.Student.FirstName, successor.Student.LastName);
                    }
                    return true;
                }
                else
                {
                    parent = current;
                    current = current.Right;
                }
            }
            else if (compareResult < 0)
            {
                parent = current;
                current = current.Left;
            }
            else
            {
                parent = current;
                current = current.Right;
            }
        }
        return false;
    }

    private TreeNode FindMin(TreeNode node)
    {
        while (node.Left != null)
        {
            node = node.Left;
        }
        return node;
    }

    public bool EditStudent(string firstName, string lastName)
    {
        TreeNode current = root;

        while (current != null)
        {
            int compareResult = String.Compare(lastName, current.Student.LastName);
            if (compareResult == 0)
            {
                if (current.Student.FirstName == firstName)
                {
                    Console.WriteLine("Введіть нове ім'я студента:");
                    string newFirstName = Console.ReadLine();
                    Console.WriteLine("Введіть нове прізвище студента:");
                    string newLastName = Console.ReadLine();
                    Console.WriteLine("Введіть нову оцінку з екзамену 1:");
                    int newExam1 = int.Parse(Console.ReadLine());
                    Console.WriteLine("Введіть нову оцінку з екзамену 2:");
                    int newExam2 = int.Parse(Console.ReadLine());
                    Console.WriteLine("Введіть нову оцінку з екзамену 3:");
                    int newExam3 = int.Parse(Console.ReadLine());

                    current.Student.FirstName = newFirstName;
                    current.Student.LastName = newLastName;
                    current.Student.Exam1 = newExam1;
                    current.Student.Exam2 = newExam2;
                    current.Student.Exam3 = newExam3;

                    return true;
                }
                else
                {
                    // Go to the next node with the same last name
                    current = current.Right;
                }
            }
            else if (compareResult < 0)
            {
                current = current.Left;
            }
            else
            {
                current = current.Right;
            }
        }
        return false;
    }

    public void DisplayStudentsInOrder()
    {
        Console.WriteLine("Список студентів:");
        InOrderTraversal(root);
    }

    private void InOrderTraversal(TreeNode node)
    {
        if (node != null)
        {
            InOrderTraversal(node.Left);
            Console.WriteLine($"Прізвище: {node.Student.LastName}, Ім'я: {node.Student.FirstName}, " +
                              $"Оцінки: {node.Student.Exam1}, {node.Student.Exam2}, {node.Student.Exam3}");
            InOrderTraversal(node.Right);
        }
    }

    public List<Student> GetSortedStudentsByName()
    {
        List<Student> students = new List<Student>();
        InOrderTraversalForSorting(root, students);
        return students;
    }

    private void InOrderTraversalForSorting(TreeNode node, List<Student> students)
    {
        if (node != null)
        {
            InOrderTraversalForSorting(node.Left, students);
            students.Add(node.Student);
            InOrderTraversalForSorting(node.Right, students);
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        BinaryTree studentTree = new BinaryTree();

        while (true)
        {
            Console.WriteLine("1. Додати студента");
            Console.WriteLine("2. Видалити студента");
            Console.WriteLine("3. Редагувати інформацію про студента");
            Console.WriteLine("4. Вивести студентів відсортованих за прізвищем");
            Console.WriteLine("5. Вихід");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddStudent(studentTree);
                    break;
                case 2:
                    RemoveStudent(studentTree);
                    break;
                case 3:
                    EditStudent(studentTree);
                    break;
                case 4:
                    DisplaySortedStudentsByName(studentTree);
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Невірний вибір.");
                    break;
            }
        }
    }

    public static void AddStudent(BinaryTree studentTree)
    {
        Console.WriteLine("Введіть ім'я студента:");
        string firstName = Console.ReadLine();
        Console.WriteLine("Введіть прізвище студента:");
        string lastName = Console.ReadLine();
        Console.WriteLine("Введіть оцінку з екзамену 1:");
        int exam1 = int.Parse(Console.ReadLine());
        Console.WriteLine("Введіть оцінку з екзамену 2:");
        int exam2 = int.Parse(Console.ReadLine());
        Console.WriteLine("Введіть оцінку з екзамену 3:");
        int exam3 = int.Parse(Console.ReadLine());

        Student newStudent = new Student
        {
            FirstName = firstName,
            LastName = lastName,
            Exam1 = exam1,
            Exam2 = exam2,
            Exam3 = exam3
        };

        studentTree.AddStudent(newStudent);
        Console.WriteLine("Студент доданий успішно.");
    }

    public static void RemoveStudent(BinaryTree studentTree)
    {
        Console.WriteLine("Введіть ім'я студента для видалення:");
        string firstName = Console.ReadLine();
        Console.WriteLine("Введіть прізвище студента для видалення:");
        string lastName = Console.ReadLine();

        if (studentTree.RemoveStudent(firstName, lastName))
        {
            Console.WriteLine("Студент видалений успішно.");
        }
        else
        {
            Console.WriteLine("Студент не знайдений.");
        }
    }

    public static void EditStudent(BinaryTree studentTree)
    {
        Console.WriteLine("Введіть ім'я студента для редагування:");
        string firstName = Console.ReadLine();
        Console.WriteLine("Введіть прізвище студента для редагування:");
        string lastName = Console.ReadLine();

        if (studentTree.EditStudent(firstName, lastName))
        {
            Console.WriteLine("Інформація про студента відредагована успішно.");
        }
        else
        {
            Console.WriteLine("Студент не знайдений.");
        }
    }

    public static void DisplaySortedStudentsByName(BinaryTree studentTree)
    {
        List<Student> sortedStudents = studentTree.GetSortedStudentsByName();
        Console.WriteLine("Студенти, відсортовані за прізвищем:");
        foreach (var student in sortedStudents)
        {
            Console.WriteLine($"Прізвище: {student.LastName}, Ім'я: {student.FirstName}, " +
                              $"Оцінки: {student.Exam1}, {student.Exam2}, {student.Exam3}");
        }
    }
}
