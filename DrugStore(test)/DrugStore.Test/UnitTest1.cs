using System.Linq;
using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DrugStore.Test
{
    [TestClass]
    public class UnitTest1
    {
        public static string TEST_DATASETS_PATH = "../../../TestDatasets";
        public static string DATASET_EMPTY_PATH = $"{TEST_DATASETS_PATH}/dataset_empty";
        public static string DATASET_FULL_PATH = $"{TEST_DATASETS_PATH}/dataset_full";
        public static Random rand = new Random();

        [TestMethod]
        public void TestReadDataset()
        {
            CreateEmptyDataset(DATASET_EMPTY_PATH);
            Store ds1 = new Store(DATASET_EMPTY_PATH);
            ds1.ReadFromDataset();

            Store ds2 = new Store(DATASET_FULL_PATH);
            ds2.ReadFromDataset();
        }
        [TestMethod]
        public void TestAddDrug()
        {
            CreateEmptyDataset(DATASET_EMPTY_PATH);
            Store ds = new Store(DATASET_EMPTY_PATH);
            ds.ReadFromDataset();
            for(int i = 0; i < 10000; i++)
            {
                ds.AddDrug($"Drug_{i}", 10000+i);
                Assert.IsTrue(ds.Drugs.ContainsKey($"Drug_{i}"));
                Store.Drug drug = ds.GetDrugInfo($"Drug_{i}");
                Assert.AreEqual(drug.PAllergies.Count + drug.NAllergies.Count, 3);
                Assert.AreEqual(drug.Effects.Count, 2);
            }
        }

        [TestMethod]
        public void TestAddDisease()
        {
            CreateEmptyDataset(DATASET_EMPTY_PATH);
            Store ds = new Store(DATASET_EMPTY_PATH);
            ds.ReadFromDataset();
            for(int i = 0; i < 10000; i++)
            {
                ds.AddDisease($"Dis_{i}");
                Assert.IsTrue(ds.Diseases.ContainsKey($"Dis_{i}"));
                Store.Disease dis = ds.GetDiseaseInfo($"Dis_{i}");
                Assert.AreEqual(dis.PAllergies.Count + dis.NAllergies.Count, 3);
            }
        }

        [TestMethod]
        public void TestDeleteDrug()
        {
            CreateEmptyDataset(DATASET_EMPTY_PATH);
            Store ds = new Store(DATASET_EMPTY_PATH);
            ds.ReadFromDataset();
            for(int i = 0; i < 10000; i++)
            {
                ds.AddDrug($"Drug_{i}", 10000+i);
            }
            for(int i = 0; i < 1000; i ++)
            {
                ds.DeleteDrug($"Drug_{i}");
                Assert.IsFalse(ds.Diseases.Values.SelectMany(d=>d.NAllergies.Concat(d.PAllergies)).Contains($"Drug_{i}"));
            }
        }
        [TestMethod]
        public void TestDeleteDisease()
        {
            CreateEmptyDataset(DATASET_EMPTY_PATH);
            Store ds = new Store(DATASET_EMPTY_PATH);
            ds.ReadFromDataset();
            for(int i = 0; i < 10000; i++)
            {
                ds.AddDisease($"Dis_{i}");
            }
            for(int i = 0; i < 1000; i ++)
            {
                ds.DeleteDisease($"Dis_{i}");
                Assert.IsFalse(ds.Drugs.Values.SelectMany(d=>d.NAllergies.Concat(d.PAllergies)).Contains($"Dis_{i}"));
            }
        }
        [TestMethod]
        public void TestWriteToDataset()
        {
            CreateEmptyDataset(DATASET_EMPTY_PATH);
            Store ds = new Store(DATASET_EMPTY_PATH);
            for(int i = 0; i < 100000; i++)
            {
                ds.AddDrug($"Drug_{i}", i);
            }
            ds.WriteToDataset();
            for(int i = 0; i < 100000; i++)
            {
                ds.AddDisease($"Dis_{i}");
            }
            ds.WriteToDataset();
        }
        public void CreateEmptyDataset(string path)
        {
            File.Create($"{path}/alergies.txt").Close();
            File.Create($"{path}/diseases.txt").Close();
            File.Create($"{path}/drugs.txt").Close();
            File.Create($"{path}/effects.txt").Close(); 
        }
    }
}
