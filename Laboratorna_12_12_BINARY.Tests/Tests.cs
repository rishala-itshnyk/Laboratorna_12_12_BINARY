namespace Laboratorna_12_12_BINARY.Tests;

[TestFixture]
public class Tests
{
    [Test]
    public void AddStudent_WhenCalled_StudentIsAdded()
    {
        // Arrange
        BinaryTree studentTree = new BinaryTree();
        Student student = new Student
        {
            FirstName = "Петро",
            LastName = "Порошенко",
            Exam1 = 85,
            Exam2 = 90,
            Exam3 = 80
        };

        // Act
        studentTree.AddStudent(student);
        var addedStudent = studentTree.GetSortedStudentsByName()[0];

        // Assert
        Assert.IsNotNull(addedStudent);
        Assert.AreEqual(student.FirstName, addedStudent.FirstName);
        Assert.AreEqual(student.LastName, addedStudent.LastName);
        Assert.AreEqual(student.Exam1, addedStudent.Exam1);
        Assert.AreEqual(student.Exam2, addedStudent.Exam2);
        Assert.AreEqual(student.Exam3, addedStudent.Exam3);
    }
}