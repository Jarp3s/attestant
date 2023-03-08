﻿using attestant;
using attestant.DataStructures;
using System.Text.RegularExpressions;
using attestant.InputReaders;

Console.OutputEncoding = System.Text.Encoding.UTF8;


List<LanguageDevelopment> languageDevelopments = SoundLawReader.GetLanguageDevelopments();
WordReconstructor wordReconstructor = new(languageDevelopments);
HashSet<string> reconstructedWords = wordReconstructor.Reconstruct("");

foreach(var word in reconstructedWords)
    Console.WriteLine(word);
