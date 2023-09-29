using ConsoleCiber1;
using System.Diagnostics;

const string filename = "text.txt";
const string alphabet = "_ОЕЁАИТНСРВЛКМДПУЯЫЭЗЬЪБГЧЙХЖЮШЦЩЭФ";

var id = 1;
var letters = alphabet.Select(x => new LetterModel() { I = id++, Name = x }).ToList();

File.Delete(filename);

if (!File.Exists(filename))
{
    Console.WriteLine("Напиши текст в файл!");

    var file = File.Create(filename);
    file.Close();

    Process.Start("notepad.exe", filename).WaitForExit();
}

var textUp = (File.ReadAllText(filename) ?? "").ToUpper()
    .Replace(' ', '_')
    .ToString();

textUp?.ToList().ForEach(l =>
{
    var letter = letters.FirstOrDefault(n => n.Name == l);
    if (letter != null)
        letter.Num++;
});

var allNumLetters = letters.Sum(x => x.Num);

var strFile = $"Num symbols: {allNumLetters}\n|  i  |  Name  |  Num  | P(i) | log2(Pi) | P(i) * log2(Pi) |\n";
letters.ForEach(l =>
{
    var idStr = l.I > 9 ? " " + l.I : l.I.ToString();
    var pi = l.Num / allNumLetters;
    var log2Pi = Math.Log(pi, 2);

    if (l.Num > 0)
    {
        strFile += $"| {idStr}  |  {l.Name}  |  {l.Num} |  {pi:F7} | {log2Pi:F7} | {pi * log2Pi:F7} |\n";
    }
    else
    {
        strFile += ($"| {idStr}  |  {l.Name}  |  {l.Num}  |    -    |   -    |  -   |\n");
    }
});

var fileRes = filename + ".res.txt";

File.WriteAllText(fileRes, strFile);
Process.Start("notepad.exe", fileRes).WaitForExit();
