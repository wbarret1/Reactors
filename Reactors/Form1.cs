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

        public Form1()
        {
            InitializeComponent();

            TreeNode m_ReactorNode = new TreeNode("Reactor");
            TreeNode m_InputsNode = new TreeNode("Input Materials");
            TreeNode m_OutputsNode = new TreeNode("Output Materials");
            TreeNode m_ChemicalsNode = new TreeNode("Chemicals");
            TreeNode m_ReactionsNode = new TreeNode("Reactions");

            r = new Reactor();

            Chemical chemA = new Chemical("A", "", 75.0, -125.0, 25.0);
            TreeNode node = new TreeNode("A");
            node.Tag = chemA;
            m_ChemicalsNode.Nodes.Add(node);
            Chemical chemB = new Chemical("B", "", 75.0, -150.0, 45.0);
            node = new TreeNode("B");
            node.Tag = chemB;
            m_ChemicalsNode.Nodes.Add(node);
            Chemical chemC = new Chemical("C", "", 75.0, -98.0, 30.0);
            node = new TreeNode("C");
            node.Tag = chemC;
            m_ChemicalsNode.Nodes.Add(node);
            Chemical chemD = new Chemical("D", "", 75.0, -190.0, 85.0);
            node = new TreeNode("D");
            node.Tag = chemD;
            m_ChemicalsNode.Nodes.Add(node);
            Chemical chemInert = new Chemical("Inert", "", 18.0, -285.0, 70.0);
            node = new TreeNode("Inert");
            node.Tag = chemInert;
            m_ChemicalsNode.Nodes.Add(node);
            Material matIn1 = new Material("Input 1");
            matIn1.AddChemical(chemA, 0.25);
            matIn1.AddChemical(chemB, 0.0);
            matIn1.AddChemical(chemC, 0.0);
            matIn1.AddChemical(chemD, 0.0);
            matIn1.AddChemical(chemInert, 0.75);
            matIn1.FlowRate = 100;
            Material matOut1 = new Material("Output 1");
            matOut1.AddChemical(chemA, 0.0);
            matOut1.AddChemical(chemB, 0.0);
            matOut1.AddChemical(chemC, 0.0);
            matOut1.AddChemical(chemD, 0.0);
            matOut1.AddChemical(chemInert, 0.0);
            Reaction react1 = new Reaction();
            react1.AddReactant(chemA, -1.0);
            react1.AddReactant(chemB, 1.0);
            r.AddInput(matIn1);
            r.AddOutput(matOut1);
            r.Reaction = react1;
            r.Reaction.Extent = 12.5;
            r.Reaction.ResidenceTime = 600;
            r.Calculate();

            m_ReactorNode.Tag = r;
            this.propertyGrid1.SelectedObject = m_ReactorNode.Tag;
            this.treeView1.Nodes.Add(m_ReactorNode);
            this.treeView1.Nodes.Add(m_ChemicalsNode);

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

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.propertyGrid1.SelectedObject = e.Node.Tag;
        }
    }
}
