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

        Queue<int> macchine;
        Queue<TaskCompletionSource<bool>> pompaQueue;

        public CDistributore(int pompe, int capacitaSerbatoio)
        {
            this.pompe = pompe;
            capacitaSerbatoioCurrent = capacitaSerbatoio;
            capacitaSerbatoioMax = capacitaSerbatoio;
            riempie = false;
            semaphoreBenza = new SemaphoreSlim(pompe, pompe);
            pompaQueue = new Queue<TaskCompletionSource<bool>>();
        }

        public async Task RichiediPompa()
        {
            await semaphoreBenza.WaitAsync();
            macchine.Enqueue(1);
        }

        public async Task Pompa(int l)
        {
            var tcs = new TaskCompletionSource<bool>();
            pompaQueue.Enqueue(tcs);

            if (capacitaSerbatoioCurrent <= l)
                // rifornisci

            capacitaSerbatoioCurrent -= l;

            pompaQueue.Dequeue();
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

        }
    }
}
