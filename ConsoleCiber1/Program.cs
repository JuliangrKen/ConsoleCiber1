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
    .Replace('\n', '_')
    .ToString();

textUp?.ToList().ForEach(l =>
{
    var letter = letters.FirstOrDefault(n => n.Name == l);
    if (letter != null)
        letter.Num++;
});

var allNumLetters = letters.Sum(x => x.Num);

var strFile = $"Num symbols: {allNumLetters}\n\n| {"i",-16} | {"Name",-16} | {"Num",-16} | {"P(i)",-16} | {"log2(Pi)",-16} | {"P(i) * log2(Pi)",-16} |\n";
letters.ForEach(l =>
{
    var idStr = l.I < 10 ? " " + l.I : l.I.ToString();
    var pi = l.Num / allNumLetters;
    var log2Pi = Math.Log(pi, 2);

    if (l.Num > 0)
    {
        strFile += $"| {idStr,-16} | {l.Name,-16} | {l.Num,-16} | {pi.ToString("F7"), -16} | {log2Pi.ToString("F7"),-16} | {(pi * log2Pi).ToString("F7"), -16} |\n";
    }
    else
    {
        strFile += $"| {idStr,-16} | {l.Name,-16} | {l.Num,-16} | {"",-16} | {"",-16} | {"",-16} |\n";
    }
});

var fileRes = filename + ".res.txt";

File.WriteAllText(fileRes, strFile);
Process.Start("notepad.exe", fileRes).WaitForExit();
