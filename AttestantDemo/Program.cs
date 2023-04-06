global using attestantResearch;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var (pc, oir, mw) = ("adrīmo", "ārəμ", "eiriv");
//                              Proto-Celtic Old-Irish Middle-Welsh 
WordSet demoSet = new(oir, mw, pc);
demoSet.GenerateTrace();
