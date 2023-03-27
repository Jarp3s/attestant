using attestant.Algorithms;
using attestant.DataStructures;
using attestant.InputReaders;

namespace attestantResearch;


public class WordSet
{
    public readonly string OldIrish;
    public readonly string MiddleWelsh;
    public readonly string ProtoCeltic;

    private readonly WordTransformer _irishTransformer;
    private readonly WordTransformer _welshTransformer;

    // The Old Irish form that will be developed using the laws
    public UNode<Word, SoundLaw> ConstructedIrish 
        => _irishTransformer.Transform(ProtoCeltic); 
    // The Middle Welsh form that will be developed using the laws
    public UNode<Word, SoundLaw> ConstructedWelsh
        => _welshTransformer.Transform(ProtoCeltic); 

    private WordSet(string oir, string mw, string pc)
    {
        OldIrish = oir;
        MiddleWelsh = mw;
        ProtoCeltic = pc;

        _irishTransformer = new WordTransformer(SoundLawReader
            .FetchDevelopment("OldIrishLaws.json").SoundLaws);
        _welshTransformer = new WordTransformer(SoundLawReader
            .FetchDevelopment("MiddleWelshLaws.json").SoundLaws);
    }

    public static WordSet Parse(string inputCognates)
    {
        var cognates = inputCognates.Split(';');

        var oldIrish = new WordTransformer(SoundLawReader
            .FetchDevelopment("OldIrishSpelling.json").SoundLaws).Transform(cognates[1].TrimStart()).Value;
        var middleWelsh = new WordTransformer(SoundLawReader
            .FetchDevelopment("MiddleWelshSpelling.json").SoundLaws).Transform(cognates[2].TrimStart()).Value;
        var protoCeltic = new WordTransformer(SoundLawReader
            .FetchDevelopment("ProtoCelticSpelling.json").SoundLaws).Transform(cognates[0]).Value;

        return new WordSet(oldIrish, middleWelsh, protoCeltic);
    }

    public override string ToString()
    {
        return
            "======================================================" +
            $"\n {ProtoCeltic} " +
            "======================================================" +
            $"\n{OldIrish} => {ConstructedIrish.Value} : ({ConstructedIrish.Value.EditDistance(OldIrish)})" + 
            $"\n{MiddleWelsh} => {ConstructedWelsh.Value} : ({ConstructedWelsh.Value.EditDistance(MiddleWelsh)})";
    }

    public void GenerateTrace()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("======================================================");
        Console.WriteLine($" {ProtoCeltic}");
        Console.WriteLine("======================================================");
        Console.WriteLine();

        var baseLength = 20;
        
        void Trace(Word wrd, SoundLaw law)
            => Console.WriteLine(
                $" {wrd} {(law == default! ? "" : new string(' ',  baseLength - wrd.Length) + $"<-| {law}")}");

        Console.ForegroundColor = ConsoleColor.White;
        UNode<Word, SoundLaw> constructedIrish = ConstructedIrish;
        constructedIrish.First.TraverseDown(Trace);
        var editDistance = $"ED: {constructedIrish.Value.EditDistance(OldIrish)}";
        Console.WriteLine($" {editDistance} {new string(' ', baseLength - editDistance.Length)}" 
                          + $"<-| {constructedIrish.Value} > {OldIrish}");
        Console.WriteLine();

        UNode<Word, SoundLaw> constructedWelsh = ConstructedWelsh;
        constructedWelsh.First.TraverseDown(Trace);
        editDistance = $"ED: {constructedWelsh.Value.EditDistance(MiddleWelsh)}";
        Console.WriteLine($" {editDistance} {new string(' ', baseLength - editDistance.Length)}" 
                          + $"<-| {constructedWelsh.Value} > {MiddleWelsh}");
        Console.WriteLine();
        Console.ResetColor();
    }
}
