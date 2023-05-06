using System.Text;
using CM_ADS;

class Program
{
    public static void Main()
    {
        StringBuilder taskString = new StringBuilder();
        taskString.AppendLine("Выберите предмет:");
        taskString.AppendLine("1) Численные методы");
        taskString.AppendLine("2) Алгоритмы и структуры данных");
        Console.WriteLine(taskString);
        
        String input = Console.ReadLine();
        Console.Clear();

        switch (input) {
            case "1":
                TestCM testCm = new TestCM();
                testCm.Test();
                break;
            
            case "2":
                TestADS testADS = new TestADS();
                testADS.Test();
                break;
            
            default:
                return;
        }
    }
}