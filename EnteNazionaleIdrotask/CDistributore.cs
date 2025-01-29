using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnteNazionaleIdrotask
{
    public class CDistributore
    {
        int pompe;
        int capacitaSerbatoioCurrent;
        int capacitaSerbatoioMax;

        bool riempie;

        SemaphoreSlim semaphoreBenza;
        SemaphoreSlim semaphoreFERMO;

        Queue<int> macchine;

        public CDistributore(int pompe, int capacitaSerbatoio)
        {
            this.pompe = pompe;
            capacitaSerbatoioCurrent = capacitaSerbatoio;
            capacitaSerbatoioMax = capacitaSerbatoio;
            riempie = false;
            semaphoreBenza = new SemaphoreSlim(pompe, pompe);
            semaphoreFERMO = new SemaphoreSlim(0, pompe);
        }

        public async Task RichiediPompa()
        {
            await semaphoreBenza.WaitAsync();
            macchine.Enqueue(1);
        }

        public async Task Pompa(int l)
        {
            while (capacitaSerbatoioCurrent <= l)
            {
                await semaphoreFERMO.WaitAsync();
                // rifornisci
            }
            capacitaSerbatoioCurrent -= l;
            await Task.Delay(1000);
        }

        public async Task RilasciaPompa()
        {
            macchine.Dequeue();
            semaphoreBenza.Release();

            if (macchine.Count == 0);
                // rifornisci
        }

        async Task rifornisci()
        {
            capacitaSerbatoioCurrent = capacitaSerbatoioMax;
            await Task.Delay(1000);

            semaphoreFERMO.Release(pompe);
        }
    }
}
