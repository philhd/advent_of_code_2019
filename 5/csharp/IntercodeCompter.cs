using System;
using System.Collections.Generic;
using System.Linq;

public class IntercodeComputer
{
    private List<int> m_state;
    private readonly List<int> m_initialState;

    private enum ParameterMode
    {
        Position = 0,
        Immediate = 1
    }

    public IntercodeComputer(IEnumerable<int> initialState)
    {
        m_initialState = initialState.ToList();
        Reset();
    }

    public void Reset()
    {
        m_state = new List<int>(m_initialState);
    }

    public void Compute(int input)
    {
        int i = 0;
        while (i < m_state.Count)
        {
            var opcode = GetOpCode(m_state[i]);
            var modes = GetModes(m_state[i]);
            if (opcode == 99)
            {
                return;
            }

            if (opcode == 1)
            {
                var result = Operate(i, (a, b) => a + b, modes);
                var resultIdx = m_state[i + 3];
                m_state[resultIdx] = result;
                i += 4;
            }
            else if (opcode == 2)
            {
                var result = Operate(i, (a, b) => a * b, modes);
                var resultIdx = m_state[i + 3];
                m_state[resultIdx] = result;
                i += 4;
            }
            else if (opcode == 3)
            {
                var resultIdx = m_state[i + 1];
                m_state[resultIdx] = input;
                i += 2;
            }
            else if (opcode == 4)
            {
                Console.WriteLine(modes[0] == ParameterMode.Immediate ? m_state[i + 1] : m_state[m_state[i + 1]]);
                i += 2;
            }
            else if (opcode == 5)
            {
                var op1 = modes[0] == ParameterMode.Immediate ? m_state[i + 1] : m_state[m_state[i + 1]];
                var op2 = modes[1] == ParameterMode.Immediate ? m_state[i + 2] : m_state[m_state[i + 2]];
                if (op1 != 0)
                {
                    i = op2;
                }
                else
                {
                    i += 3;
                }
            }
            else if (opcode == 6)
            {
                var op1 = modes[0] == ParameterMode.Immediate ? m_state[i + 1] : m_state[m_state[i + 1]];
                var op2 = modes[1] == ParameterMode.Immediate ? m_state[i + 2] : m_state[m_state[i + 2]];
                if (op1 == 0)
                {
                    i = op2;
                }
                else
                {
                    i += 3;
                }
            }
            else if (opcode == 7)
            {
                var result = Operate(i, (a, b) => a < b ? 1 : 0, modes);
                var resultIdx = m_state[i + 3];
                m_state[resultIdx] = result;
                i += 4;
            }
            else if (opcode == 8)
            {
                var result = Operate(i, (a, b) => a == b ? 1 : 0, modes);
                var resultIdx = m_state[i + 3];
                m_state[resultIdx] = result;
                i += 4;
            }
        }
    }

    private int Operate(int i, Func<int, int, int> operation, List<ParameterMode> modes)
    {
        var op1 = modes[0] == ParameterMode.Immediate ? m_state[i + 1] : m_state[m_state[i + 1]];
        var op2 = modes[1] == ParameterMode.Immediate ? m_state[i + 2] : m_state[m_state[i + 2]];
        return operation(op1, op2);
    }

    private int GetOpCode(int instruction)
    {
        var instString = instruction.ToString();
        if (instString.Length < 2)
        {
            return instruction;
        }

        return int.Parse(instString.Substring(instString.Length - 2));
    }

    private List<ParameterMode> GetModes(int instruction)
    {
        var instString = instruction.ToString();
        if (instString.Length <= 2)
        {
            return new List<ParameterMode> { ParameterMode.Position, ParameterMode.Position, ParameterMode.Position };
        }

        var modes = instString.Reverse().Skip(2).Select(x => (ParameterMode)Enum.Parse(typeof(ParameterMode), x.ToString())).ToList();

        while (modes.Count < 3)
        {
            modes.Add(ParameterMode.Position);
        }

        return modes;
    }

    public int GetOutput()
    {
        return m_state[0];
    }
}