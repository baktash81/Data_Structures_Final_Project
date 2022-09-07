using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DrugStore
{

    public class Store
    {
        public Store(string dataset_location_folder)
        {
            this.DATASET_LOCATION_FOLDER = dataset_location_folder;
            this.Drugs = new Dictionary<string, Drug>();
            this.Diseases = new Dictionary<string, Disease>();
        }
        public readonly string DATASET_LOCATION_FOLDER;
        public class Drug
        {  
            public Drug(string name, int price)
            {
                this.Name = name;
                this.Price = price;
                this.Effects = new List<(string drug, string effect)>();
                PAllergies = new List<string>();
                NAllergies = new List<string>();
            }
            public int Price = 0;
            public string Name = "";
            public List<(string drug, string effect)> Effects;
            public List<string> PAllergies;
            public List<string> NAllergies;

            public void AddAllergiy(string disease, string altype)
            {
                if(altype == "+")
                    this.PAllergies.Add(disease);
                else if (altype == "-")
                    this.NAllergies.Add(disease);
                else
                    throw new InvalidDataException("Unknown allergy type");
            }

            public void DeleteEffect(string drug, string effect)
            {
                this.Effects.Remove((drug:drug, effect:effect));
            }

        }

        public class Disease
        {
            public Disease(string name)
            {
                this.Name = name;
                this.NAllergies = new List<string>();
                this.PAllergies = new List<string>();
            }
            public string Name;
            public List<string> PAllergies;
            public List<string> NAllergies;
            public void AddAllergiy(string drug, string altype)
            {
                if(altype == "+")
                    this.PAllergies.Add(drug);
                else if (altype == "-")
                    this.NAllergies.Add(drug);
                else
                    throw new InvalidDataException("Unknown allergy type");
            }
        }

        public Dictionary<string, Drug> Drugs;
        public Dictionary<string, Disease> Diseases;

        public void ReadFromDataset()
        {
            ReadDrugs();
            ReadDiseases();
            ReadEffects();
            ReadAllergies();
        }

        public void WriteToDataset()
        {
            WriteDrugs();
            WriteDiseases();
            WriteEffects();
            WriteAllergies();
        }

        private void WriteDrugs()
        {
            using(var writer = new StreamWriter($"{this.DATASET_LOCATION_FOLDER}/drugs.txt"))
            {
                foreach(var item in this.Drugs)
                {
                    string drug = item.Key;
                    int price = item.Value.Price;
                    writer.WriteLine($"{drug} : {price}");
                }
            }
        }
        private void WriteDiseases()
        {
            using(var writer = new StreamWriter($"{this.DATASET_LOCATION_FOLDER}/diseases.txt"))
            {
                foreach(var item in this.Diseases)
                {
                    string disease = item.Key;
                    writer.WriteLine($"{disease}");
                }
            }
        }
        private void WriteEffects()
        {
            using(var writer = new StreamWriter($"{this.DATASET_LOCATION_FOLDER}/effects.txt"))
            {
                foreach(var item in this.Drugs)
                {
                    Drug drug = item.Value;
                    if(drug.Effects.Count == 0)
                        continue;
                    writer.WriteLine($"{drug.Name} : {string.Join(" ; ", drug.Effects)}");
                }
            }
        }
        private void WriteAllergies()
        {
            using(var writer = new StreamWriter($"{this.DATASET_LOCATION_FOLDER}/alergies.txt"))
            {
                foreach(var item in this.Diseases)
                {
                    Disease disease = item.Value;
                    if (disease.NAllergies.Count == 0 && disease.PAllergies.Count==0)
                        continue;
                    else
                    {
                        writer.WriteLine($"{disease.Name} : {string.Join(" ; ", disease.PAllergies.Select(x=>(x,"+")).Concat(disease.NAllergies.Select(x=>(x, "-"))))}");
                    }
                }
            }
        }
        private void ReadDrugs()
        {
            using(var reader = new StreamReader($"{this.DATASET_LOCATION_FOLDER}/drugs.txt"))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    if(line == string.Empty)
                        continue;
                    
                    var temp = line.Split(":").Select(x=>x.Trim()).ToArray();
                    this.Drugs.Add(temp[0], new Drug(temp[0], int.Parse(temp[1])));
                }
            }
        }
        private void ReadDiseases()
        {
            using(var reader = new StreamReader($"{this.DATASET_LOCATION_FOLDER}/diseases.txt"))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    if(line == string.Empty)
                        continue;
                    
                    var temp = line.Trim();
                    this.Diseases.Add(temp, new Disease(temp));
                }
            }
        }

        private void ReadEffects()
        {
            using(var reader = new StreamReader($"{this.DATASET_LOCATION_FOLDER}/effects.txt"))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    if(line == string.Empty)
                        continue;
                    
                    var temp = line.Split(":").Select(x=>x.Trim()).ToArray();
                    string basedrug = temp[0];
                    var effects = temp[1].Split(";").Select(x=>x.Trim()).Select(eff=>{
                        var temp = eff.Trim(')', '(', ' ').Split(",").Select(x=>x.Trim()).ToArray();
                        return (drug:temp[0], effname:temp[1]);
                    }).ToArray();

                    foreach(var eff in effects)
                        this.Drugs[basedrug].Effects.Add(eff);
                }
            }
        }

        private void ReadAllergies()
        {
            using(var reader = new StreamReader($"{this.DATASET_LOCATION_FOLDER}/alergies.txt"))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    if(line == string.Empty)
                        continue;
                    
                    var temp = line.Split(":").Select(x=>x.Trim()).ToArray();
                    string disease = temp[0];
                    var allergies = temp[1].Split(";").Select(x=>x.Trim()).Select(al=>{
                        var temp = al.Trim(')', '(', ' ').Split(",").Select(x=>x.Trim()).ToArray();
                        return (drug:temp[0], altype:temp[1]);
                    }).ToArray();
                    foreach(var al in allergies)
                        if(al.altype == "+")
                        {
                            this.Diseases[disease].PAllergies.Add(al.drug);
                            this.Drugs[al.drug].PAllergies.Add(disease);
                        }
                        else if (al.altype == "-")
                        {
                            this.Diseases[disease].NAllergies.Add(al.drug);
                            this.Drugs[al.drug].NAllergies.Add(disease);
                        }
                        else
                            throw new InvalidDataException("Unknown allergy type!");
                }
            }
        }
        private string RandomString()
        {
            Random rand = new Random();
            string str = "abcdefghijklmnopqrstuvwxyz";
            string result = "";
            for (int i =0; i < 10; i++)
                result += str[rand.Next(0, str.Length)];
            return result;
        }
        private string RandomDrug()
        {
            Random rand = new Random();
            if(this.Drugs.Keys.Count < 100)
            {
                Drug drug = new Drug($"Drug_{RandomString()}", rand.Next(10000, 100000));
                this.Drugs.Add(drug.Name, drug);
                return drug.Name;
            }
            return this.Drugs.Keys.ElementAt(rand.Next(0, Math.Min(100, this.Drugs.Count)));
        }

        private string RandomDisease()

        {
            if (this.Diseases.Keys.Count < 100)
            {
                Disease dis = new Disease($"Dis_{RandomString()}");
                this.Diseases.Add(dis.Name, dis);
                return dis.Name;
            }
            Random rand = new Random();
            return this.Diseases.Keys.ElementAt(rand.Next(0, 100));
        }

        private string RandomEffect()
        {
            return $"Eff_{RandomString()}";
        }

        private string RandomAllergyType()
        {
            Random rand = new Random();
            return new string[2]{"+", "-"}[rand.Next(0,2)];
        }
        public Drug GetDrugInfo(string drugName)
        {
            if(!this.Drugs.ContainsKey(drugName))
                throw new ArgumentException("Drug does not exist!");
            Drug drug = this.Drugs[drugName];
            return drug;
        }

        public Drug AddDrug(string name, int price)
        {
            if(this.Drugs.ContainsKey(name))
                throw new ArgumentException("Drug already exist!");
            Drug drug = new Drug(name, price);
            var effs = new (string drug, string effect)[2].Select(x=>(drug:RandomDrug(), effect:RandomEffect())).ToArray();
            var allergies = new (string disease, string altype)[3].Select(x=>(disease:RandomDisease(), altype:RandomAllergyType()));
            foreach(var eff in effs)
            {
                drug.Effects.Add(eff);
                this.Drugs[eff.drug].Effects.Add((drug:drug.Name, effect:eff.effect));
            }

            foreach(var al in allergies)
            {
                if (al.altype == "+")
                {
                    drug.PAllergies.Add(al.disease);
                    this.Diseases[al.disease].PAllergies.Add(drug.Name);
                }
                else if (al.altype == "-")
                {
                    drug.NAllergies.Add(al.disease);
                    this.Diseases[al.disease].NAllergies.Add(drug.Name);
                }
            }

            this.Drugs.Add(name, drug);
            return drug;
        }

        public void DeleteDrug(string name)
        {
            if(!this.Drugs.ContainsKey(name))
                throw new ArgumentException("Drug does not exist!");
            var drug = this.Drugs[name];
            foreach(var eff in drug.Effects)
            {
                this.Drugs[eff.drug].DeleteEffect(drug:name, effect:eff.effect);
            }
            foreach(var nAl in drug.NAllergies)
            {
                this.Diseases[nAl].NAllergies.Remove(name);
            }
            foreach(var pAl in drug.PAllergies)
            {
                this.Diseases[pAl].PAllergies.Remove(name);
            }
            this.Drugs.Remove(name);
        }

        public Disease GetDiseaseInfo(string name)
        {
            if(!this.Diseases.ContainsKey(name))
                throw new ArgumentException("Disease does not exist!");

            return this.Diseases[name];
        }

        public Disease AddDisease(string name)
        {
            if(this.Diseases.ContainsKey(name))
                throw new ArgumentException("Drug already exist!");

            Disease disease = new Disease(name);
            var allergies = new (string drug, string altype)[3].Select(x=>(drug:RandomDrug(), altype:RandomAllergyType()));
            foreach(var al in allergies)
            {
                if (al.altype == "+")
                {
                    disease.PAllergies.Add(al.drug);
                    this.Drugs[al.drug].PAllergies.Add(disease.Name);
                }
                else if (al.altype == "-")
                {
                    disease.NAllergies.Add(al.drug);
                    this.Drugs[al.drug].NAllergies.Add(disease.Name);
                }
            }

            this.Diseases.Add(name, disease);
            return disease;
        }

        public void DeleteDisease(string name)
        {
            if(!this.Diseases.ContainsKey(name))
                throw new ArgumentException("Disease does not exist!");

            var disease = this.Diseases[name];
            foreach(var nAl in disease.NAllergies)
                this.Drugs[nAl].NAllergies.Remove(name);
            foreach(var pAl in disease.PAllergies)
                this.Drugs[pAl].PAllergies.Remove(name);
            
            this.Diseases.Remove(name);
        }
    }
}