using System;
using System.Collections.Generic;
using System.Linq;

public class IntercodeComputer
{
    private List<int> m_state;
    private readonly List<int> m_initialState;

    public IntercodeComputer(IEnumerable<int> initialState)
    {
        m_initialState = initialState.ToList();
        Reset();
    }

    public void SetInputs(int noun, int verb)
    {
        m_state[1] = noun;
        m_state[2] = verb;
    }

    public void Reset()
    {
        m_state = new List<int>(m_initialState);
    }

    public void Compute()
    {
        for (int i = 0; i < m_state.Count; i += 4)
        {
            var opcode = m_state[i];
            if (opcode == 99)
            {
                return;
            }
            var op1 = m_state[m_state[i + 1]];
            var op2 = m_state[m_state[i + 2]];
            var resultIdx = m_state[i + 3];
            switch (opcode)
            {
                case 1:
                    m_state[resultIdx] = op1 + op2;
                    break;
                case 2:
                    m_state[resultIdx] = op1 * op2;
                    break;
                default:
                    throw new ArgumentException("invalid op code");
            }
        }
    }

    public int GetOutput(){
        return m_state[0];
    }
}