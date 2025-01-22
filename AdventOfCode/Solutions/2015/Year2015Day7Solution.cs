using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions._2015;
internal class Year2015Day7Solution : IDaySolution
{
    public string GetFirstPartAnswer(string inputData)
    {
        var instructions = inputData.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var wiresByName = FillWireDictionary(instructions);

        return wiresByName["a"].GetSignal().ToString();
    }

    public string GetSecondPartAnswer(string inputData)
    {
        var instructions = inputData.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var wiresByName = FillWireDictionary(instructions);

        var signalWireA = wiresByName["a"].GetSignal();
        foreach (var wire in wiresByName.Values)
        {
            wire.ResetSignal();
        }
        wiresByName["b"].Instruction = signalWireA.ToString();

        return wiresByName["a"].GetSignal().ToString();
    }

    private Dictionary<string, Wire> FillWireDictionary(string[] instructions)
    {
        var wiresByName = new Dictionary<string, Wire>();

        foreach (var instruction in instructions)
        {
            var instructionParts = instruction.Split(" -> ");
            var wire = new Wire(instructionParts[0], wiresByName);
            wiresByName.Add(instructionParts[1], wire);
        }

        return wiresByName;
    }

    internal class Wire
    {
        public string Instruction { get; set; }

        private int? Signal { get; set; }

        private readonly Dictionary<string, Wire> _wiresByName;

        public Wire(string instruction, Dictionary<string, Wire> wiresByName)
        {
            Instruction = instruction;
            _wiresByName = wiresByName;
        }

        public int GetSignal()
        {
            if (Signal != null)
            {
                return Signal.Value;
            }

            if (int.TryParse(Instruction, out var value))
            {
                Signal = value;
                return Signal.Value;
            }

            if (Instruction.Contains("AND"))
            {
                var instructionParts = Instruction.Split(" AND ");

                Signal = TryGetWireSignal(instructionParts[0]) & TryGetWireSignal(instructionParts[1]);
                return Signal.Value;
            }

            if (Instruction.Contains("OR"))
            {
                var instructionParts = Instruction.Split(" OR ");

                Signal = TryGetWireSignal(instructionParts[0]) | TryGetWireSignal(instructionParts[1]);
                return Signal.Value;
            }

            if (Instruction.Contains("SHIFT"))
            {
                var shiftValue = int.Parse(Regex.Match(Instruction, "\\d+").Value);
                var wireName = Instruction.Split(" ")[0];
                var wire = _wiresByName[wireName];

                if (Instruction.Contains("L"))
                {
                    Signal = wire.GetSignal() << shiftValue;
                }
                else
                {
                    Signal = wire.GetSignal() >> shiftValue;
                }

                return Signal.Value;
            }

            if (Instruction.Contains("NOT"))
            {
                var wireName = Instruction.Split(" ")[1];
                var wire = _wiresByName[wireName];

                Signal = ~wire.GetSignal();

                return Signal.Value;
            }

            Signal = _wiresByName[Instruction].GetSignal();
            return Signal.Value;
        }

        public void ResetSignal()
        {
            Signal = null;
        }

        private int TryGetWireSignal(string potentialWireName)
        {
            if (!int.TryParse(potentialWireName, out var wireValue))
            {
                wireValue = _wiresByName[potentialWireName].GetSignal();
            }

            return wireValue;
        }
    }
}
