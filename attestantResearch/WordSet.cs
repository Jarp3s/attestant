using System.Text.Json;
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

    public WordSet(string oir, string mw, string pc)
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

        var baseLength = 15;
        
        void Trace(Word wrd, SoundLaw law)
            => Console.WriteLine(
                $" {wrd} {(law == default! ? "" : new string(' ',  baseLength - wrd.ToString().Length) + $"<-| {law}")}");

        Console.ForegroundColor = ConsoleColor.White;
        UNode<Word, SoundLaw> constructedIrish = ConstructedIrish;
        constructedIrish.First.TraverseDown(Trace);
        var editDistance = $"{Math.Round(constructedIrish.Value.NormalizedEditDistance(OldIrish), 2)}";
        Console.WriteLine($" ED: {editDistance}{editDistance.Length switch { 4 => "", 3 => "0", _ => ",00"}}" +
                          $"{new string(' ', baseLength - 7)}<-| {constructedIrish.Value} > {OldIrish}");
        Console.WriteLine();

        UNode<Word, SoundLaw> constructedWelsh = ConstructedWelsh;
        constructedWelsh.First.TraverseDown(Trace);
        editDistance = $"{Math.Round(constructedWelsh.Value.NormalizedEditDistance(MiddleWelsh), 2)}";
        Console.WriteLine($" ED: {editDistance}{editDistance.Length switch { 4 => "", 3 => "0", _ => ",00"}}" +
                          $"{new string(' ', baseLength - 7)}<-| {constructedWelsh.Value} > {MiddleWelsh}");
        Console.WriteLine();
        Console.ResetColor();
    }

    public object ToTrace()
    {
        List<string> irishDevelopments = new();
        UNode<Word, SoundLaw> wordDevelopment = ConstructedIrish.First;
        while (wordDevelopment.Next is not null)
        {
            wordDevelopment = wordDevelopment.Next;
            irishDevelopments.Add($"{wordDevelopment.Value} - {wordDevelopment.Label}");
        }
        var irishEd = wordDevelopment.Value.NormalizedEditDistance(OldIrish);

        List<string> welshDevelopments = new();
        wordDevelopment = ConstructedWelsh.First;
        while (wordDevelopment.Next is not null)
        {
            wordDevelopment = wordDevelopment.Next;
            welshDevelopments.Add($"{wordDevelopment.Value} - {wordDevelopment.Label}");
        }
        var welshEd = wordDevelopment.Value.NormalizedEditDistance(MiddleWelsh);

        var constructedSet = new { protoCeltic = ProtoCeltic
            , irishTrace = irishDevelopments, oldIrish = OldIrish, irishED = irishEd 
            , welshTrace = welshDevelopments, middleWelsh = MiddleWelsh, welshED = welshEd };

        return constructedSet;
    }
}
