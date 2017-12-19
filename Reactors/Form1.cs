using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reactors
{
    public partial class Form1 : Form
    {
        Reactor r;
        //ChemicalCollection chemicals;

        TreeNode m_ReactorNode;
        TreeNode m_InputsNode;
        TreeNode m_OutputsNode;
        TreeNode m_ChemicalsNode;
        TreeNode m_ReactionsNode;

        public Form1()
        {
            InitializeComponent();

            m_ReactorNode = new TreeNode("Reactor");
            this.treeView1.Nodes.Add(m_ReactorNode);
            m_InputsNode = new TreeNode("Input Materials");
            this.treeView1.Nodes.Add(m_InputsNode);
            m_OutputsNode = new TreeNode("Output Materials");
            this.treeView1.Nodes.Add(m_OutputsNode);
            m_ChemicalsNode = new TreeNode("Chemicals");
            this.treeView1.Nodes.Add(m_ChemicalsNode);
            m_ReactionsNode = new TreeNode("Reactions");
            this.treeView1.Nodes.Add(m_ReactionsNode);

            this.CreateChemicals();
            r = new Reactor("Reactor1");

            Material matIn1 = new Material("Input 1");
            matIn1.AddChemical(this.GetChemical("A"), 0.25);
            matIn1.AddChemical(this.GetChemical("B"), 0.0);
            matIn1.AddChemical(this.GetChemical("C"), 0.0);
            matIn1.AddChemical(this.GetChemical("D"), 0.0);
            matIn1.AddChemical(this.GetChemical("Inert"), 0.75);
            matIn1.FlowRate = 100;
            Material matOut1 = new Material("Output 1");
            matOut1.AddChemical(this.GetChemical("A"), 0.0);
            matOut1.AddChemical(this.GetChemical("B"), 0.0);
            matOut1.AddChemical(this.GetChemical("C"), 0.0);
            matOut1.AddChemical(this.GetChemical("D"), 0.0);
            matOut1.AddChemical(this.GetChemical("Inert"), 0.0);
            Reaction react1 = new Reaction();
            react1.AddReactant(this.GetChemical("A"), -1.0);
            react1.AddReactant(this.GetChemical("B"), 1.0);
            TreeNode node = new TreeNode(react1.ToString());
            node.Tag = react1;
            m_ReactionsNode.Nodes.Add(node);

            r.AddInput(matIn1);
            r.AddOutput(matOut1);
            r.Reaction = react1;
            r.Reaction.Extent = 0.25;
            r.Reaction.ResidenceTime = 600;
            r.Calculate();

            m_ReactorNode.Tag = r;
            this.propertyGrid1.SelectedObject = m_ReactorNode.Tag;

            m_ReactorNode.Nodes.Add(m_InputsNode);
            foreach (Material m in r.Inputs)
            {
                node = new TreeNode(m.Name);
                node.Tag = m;
                m_InputsNode.Nodes.Add(node);
            }
            m_ReactorNode.Nodes.Add(m_OutputsNode);
            foreach (Material m in r.Outputs)
            {
                node = new TreeNode(m.Name);
                node.Tag = m;
                m_OutputsNode.Nodes.Add(node);
            }

           
        }

        void CreateChemicals()
        {
            this.AddChemical("A", "", 75.0, -125.0, 25.0);
            this.AddChemical("B", "", 75.0, -150.0, 45.0);
            this.AddChemical("C", "", 60.0, -98.0, 30.0);
            this.AddChemical("D", "", 80.0, -190.0, 85.0);
            this.AddChemical("Inert", "", 18.0, -285.0, 70.0);
        }

        void AddChemical(string name, string casNo, double MolWt, double heatOfFormation, double entropyOfFormation)
        {
            Chemical chem = new Chemical(name, casNo, MolWt, heatOfFormation, entropyOfFormation);
            Chemicals.Add(chem);
            TreeNode node = new TreeNode(chem.Name);
            node.Tag = chem;
            m_ChemicalsNode.Nodes.Add(node);
        }

        public Chemical GetChemical(string name)
        {
            foreach (Chemical c in Chemicals.AllChemcials)
            {
                if (c.Name == name) return c;
            }
            return null;
        }

        //void CreateReactor(string name)
        //{
        //    r = new Reactor(name);

        //    Chemical chemA = new Chemical("A", "", 75.0, -125.0, 25.0);
        //    TreeNode node = new TreeNode("A");
        //    node.Tag = chemA;
        //    m_ChemicalsNode.Nodes.Add(node);
        //    Chemical chemB = new Chemical("B", "", 75.0, -150.0, 45.0);
        //    node = new TreeNode("B");
        //    node.Tag = chemB;
        //    m_ChemicalsNode.Nodes.Add(node);
        //    Chemical chemC = new Chemical("C", "", 75.0, -98.0, 30.0);
        //    node = new TreeNode("C");
        //    node.Tag = chemC;
        //    m_ChemicalsNode.Nodes.Add(node);
        //    Chemical chemD = new Chemical("D", "", 75.0, -190.0, 85.0);
        //    node = new TreeNode("D");
        //    node.Tag = chemD;
        //    m_ChemicalsNode.Nodes.Add(node);
        //    Chemical chemInert = new Chemical("Inert", "", 18.0, -285.0, 70.0);
        //    node = new TreeNode("Inert");
        //    node.Tag = chemInert;
        //    m_ChemicalsNode.Nodes.Add(node);
        //    Material matIn1 = new Material("Input 1");
        //    matIn1.AddChemical(chemA, 0.75);
        //    matIn1.AddChemical(chemB, 0.0);
        //    matIn1.AddChemical(chemC, 0.0);
        //    matIn1.AddChemical(chemD, 0.0);
        //    matIn1.AddChemical(chemInert, 0.25);
        //    matIn1.FlowRate = 100;
        //    Material matOut1 = new Material("Output 1");
        //    matOut1.AddChemical(chemA, 0.0);
        //    matOut1.AddChemical(chemB, 0.0);
        //    matOut1.AddChemical(chemC, 0.0);
        //    matOut1.AddChemical(chemD, 0.0);
        //    matOut1.AddChemical(chemInert, 0.0);
        //    Reaction react1 = new Reaction();
        //    react1.AddReactant(chemA, -1.0);
        //    react1.AddReactant(chemB, 1.0);
        //    r.AddInput(matIn1);
        //    r.AddOutput(matOut1);
        //    r.Reaction = react1;
        //    r.Reaction.Extent = 0.05;
        //    r.Reaction.ResidenceTime = 600;
        //    r.Calculate();

        //    m_ReactorNode.Tag = r;
        //    this.propertyGrid1.SelectedObject = m_ReactorNode.Tag;
        //    this.treeView1.Nodes.Add(m_ReactorNode);
        //    this.treeView1.Nodes.Add(m_ChemicalsNode);

        //    m_ReactorNode.Nodes.Add(m_InputsNode);
        //    foreach (Material m in r.Inputs)
        //    {
        //        node = new TreeNode(m.Name);
        //        node.Tag = m;
        //        m_InputsNode.Nodes.Add(node);
        //    }
        //    m_ReactorNode.Nodes.Add(m_OutputsNode);
        //    foreach (Material m in r.Outputs)
        //    {
        //        node = new TreeNode(m.Name);
        //        node.Tag = m;
        //        m_OutputsNode.Nodes.Add(node);
        //    }
        //}

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.propertyGrid1.SelectedObject = e.Node.Tag;
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            foreach(TreeNode node in m_ReactorNode.Nodes)
            {
                //((Reactor)node.Tag).Calculate();
            }
            this.propertyGrid1.Invalidate();
        }
    }
}
