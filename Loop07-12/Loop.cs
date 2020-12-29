using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;

namespace Loop07_12
{
    internal class HabrExampleConfig : ManualConfig
    {
        public HabrExampleConfig ()
        {
            Add(StatisticColumn.Median);
            Add(StatisticColumn.StdDev);
            Add(StatisticColumn.Mean);
        }
    }
    
    [Config(typeof(HabrExampleConfig ))]
    public class LoopClass
    {
        public static void SLoop()
        {
            string s = "";
            for (int i = 0; i < 1; i++)
            {
                s += "1";
            }
        }
        public static void SDLoop()
        {
            dynamic s = "";
            for (int i = 0; i < 1; i++)
            {
                s += "1";
            }
        }
        [Benchmark(Description = "SimpleMethod")]
        public void Loop()
        {
            string s = "";
            for (int i = 0; i < 1; i++)
            {
                s += "1";
            }
        }
        [Benchmark(Description = "DynamicMethod")]
        public void DLoop()
        {
            dynamic s = "";
            for (int i = 0; i < 1; i++)
            {
                s += "1";
            }
        }
        [Benchmark(Description = "StaticSimpleMethod")]
        public void SSLoop()
        {
            SLoop();
        }
        [Benchmark(Description = "StaticDynamicMethod")]
        public void SDDLoop()
        {
            SDLoop();
        }
        [Benchmark(Description = "VirtualMethod")]
        public virtual void VLoop()
        {
            string s = "";
            for (int i = 0; i < 10; i++)
            {
                s += "1";
            }
        }
        [Benchmark(Description = "DynamicVirtualMethod")]
        public virtual void DVLoop()
        {
            dynamic s = "";
            for (int i = 0; i < 10; i++)
            {
                s += "1";
            }
        }
        [Benchmark(Description = "GenericMethod")]
        public void GLoop()
        {
            GPLoop("1");
        }
        [Benchmark(Description = "DynamicGenericMethod")]
        public void GDLoop()
        {
            GPDLoop("1");
        }
        private void GPLoop<T>(T x)
        {
            string s = "";
            for (int i = 0; i < 10; i++)
            {
                s += x;
            }
        }
        private void GPDLoop<T>(T x)
        {
            dynamic s = "";
            for (int i = 0; i < 10; i++)
            {
                s += x;
            }
        }
        [Benchmark(Description = "ReflectionMethod")]
        public void RLoop()
        {
            Type type = this.GetType();
            var methodInfo = type.GetMethod("Loop").Invoke(this, null);
        }
        [Benchmark(Description = "ReflectionStaticMethod")]
        public void RSLoop()
        {
            Type type = this.GetType();
            var methodInfo = type.GetMethod("SSLoop").Invoke(this, null);
        }
        [Benchmark(Description = "ReflectionVirtualMethod")]
        public void RVLoop()
        {
            Type type = this.GetType();
            var methodInfo = type.GetMethod("VLoop").Invoke(this, null);
        }
        [Benchmark(Description = "ReflectionVirtualMethod")]
        public void RGLoop()
        {
            Type type = this.GetType();
            var methodInfo = type.GetMethod("GLoop").Invoke(this, null);
        }
        [Benchmark(Description = "ReflectionDynamicMethod")]
        public void RDLoop()
        {
            Type type = this.GetType();
            var methodInfo = type.GetMethod("DLoop").Invoke(this, null);
        }
    }
}