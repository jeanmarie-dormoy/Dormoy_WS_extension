namespace WindowsFormsClient
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboboxVille = new System.Windows.Forms.ComboBox();
            this.labelVille = new System.Windows.Forms.Label();
            this.labelInfos = new System.Windows.Forms.Label();
            this.labelAdresseDepart = new System.Windows.Forms.Label();
            this.textBoxAdresseDepart = new System.Windows.Forms.TextBox();
            this.buttonCalcul = new System.Windows.Forms.Button();
            this.textBoxAdresseArrivee = new System.Windows.Forms.TextBox();
            this.labelAdresseArrivee = new System.Windows.Forms.Label();
            this.textBoxDebug = new System.Windows.Forms.TextBox();
            this.labelAide = new System.Windows.Forms.Label();
            this.buttonAide = new System.Windows.Forms.Button();
            this.bingMapElement = new System.Windows.Forms.Integration.ElementHost();
            this.mainWindow1 = new WindowsFormsClient.MainWindow();
            this.checkBoxAlternativeRoute = new System.Windows.Forms.CheckBox();
            this.comboBoxChoiceTourism = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboboxVille
            // 
            this.comboboxVille.FormattingEnabled = true;
            this.comboboxVille.Location = new System.Drawing.Point(97, 47);
            this.comboboxVille.Name = "comboboxVille";
            this.comboboxVille.Size = new System.Drawing.Size(121, 24);
            this.comboboxVille.TabIndex = 1;
            this.comboboxVille.SelectedIndexChanged += new System.EventHandler(this.comboboxVille_SelectedIndexChanged);
            // 
            // labelVille
            // 
            this.labelVille.AutoSize = true;
            this.labelVille.Location = new System.Drawing.Point(41, 50);
            this.labelVille.Name = "labelVille";
            this.labelVille.Size = new System.Drawing.Size(34, 17);
            this.labelVille.TabIndex = 2;
            this.labelVille.Text = "Ville";
            // 
            // labelInfos
            // 
            this.labelInfos.AutoSize = true;
            this.labelInfos.Location = new System.Drawing.Point(504, 50);
            this.labelInfos.Name = "labelInfos";
            this.labelInfos.Size = new System.Drawing.Size(38, 17);
            this.labelInfos.TabIndex = 5;
            this.labelInfos.Text = "Infos";
            // 
            // labelAdresseDepart
            // 
            this.labelAdresseDepart.AutoSize = true;
            this.labelAdresseDepart.Location = new System.Drawing.Point(41, 102);
            this.labelAdresseDepart.Name = "labelAdresseDepart";
            this.labelAdresseDepart.Size = new System.Drawing.Size(107, 17);
            this.labelAdresseDepart.TabIndex = 9;
            this.labelAdresseDepart.Text = "Adresse Départ";
            // 
            // textBoxAdresseDepart
            // 
            this.textBoxAdresseDepart.Location = new System.Drawing.Point(154, 99);
            this.textBoxAdresseDepart.Name = "textBoxAdresseDepart";
            this.textBoxAdresseDepart.Size = new System.Drawing.Size(275, 22);
            this.textBoxAdresseDepart.TabIndex = 10;
            // 
            // buttonCalcul
            // 
            this.buttonCalcul.Location = new System.Drawing.Point(154, 228);
            this.buttonCalcul.Name = "buttonCalcul";
            this.buttonCalcul.Size = new System.Drawing.Size(233, 25);
            this.buttonCalcul.TabIndex = 11;
            this.buttonCalcul.Text = "Calcul Itinéraire";
            this.buttonCalcul.UseVisualStyleBackColor = true;
            this.buttonCalcul.Click += new System.EventHandler(this.buttonCalcul_Click);
            // 
            // textBoxAdresseArrivee
            // 
            this.textBoxAdresseArrivee.Location = new System.Drawing.Point(154, 142);
            this.textBoxAdresseArrivee.Name = "textBoxAdresseArrivee";
            this.textBoxAdresseArrivee.Size = new System.Drawing.Size(275, 22);
            this.textBoxAdresseArrivee.TabIndex = 13;
            // 
            // labelAdresseArrivee
            // 
            this.labelAdresseArrivee.AutoSize = true;
            this.labelAdresseArrivee.Location = new System.Drawing.Point(41, 145);
            this.labelAdresseArrivee.Name = "labelAdresseArrivee";
            this.labelAdresseArrivee.Size = new System.Drawing.Size(109, 17);
            this.labelAdresseArrivee.TabIndex = 12;
            this.labelAdresseArrivee.Text = "Adresse Arrivée";
            // 
            // textBoxDebug
            // 
            this.textBoxDebug.Location = new System.Drawing.Point(507, 140);
            this.textBoxDebug.Name = "textBoxDebug";
            this.textBoxDebug.Size = new System.Drawing.Size(275, 22);
            this.textBoxDebug.TabIndex = 14;
            // 
            // labelAide
            // 
            this.labelAide.AutoSize = true;
            this.labelAide.Location = new System.Drawing.Point(504, 214);
            this.labelAide.Name = "labelAide";
            this.labelAide.Size = new System.Drawing.Size(46, 17);
            this.labelAide.TabIndex = 18;
            this.labelAide.Text = "label1";
            // 
            // buttonAide
            // 
            this.buttonAide.Location = new System.Drawing.Point(507, 185);
            this.buttonAide.Name = "buttonAide";
            this.buttonAide.Size = new System.Drawing.Size(138, 25);
            this.buttonAide.TabIndex = 19;
            this.buttonAide.Text = "Aide -->";
            this.buttonAide.UseVisualStyleBackColor = true;
            this.buttonAide.Click += new System.EventHandler(this.buttonAide_Click);
            // 
            // bingMapElement
            // 
            this.bingMapElement.Location = new System.Drawing.Point(44, 407);
            this.bingMapElement.Name = "bingMapElement";
            this.bingMapElement.Size = new System.Drawing.Size(1859, 605);
            this.bingMapElement.TabIndex = 20;
            this.bingMapElement.Text = "elementHost1";
            this.bingMapElement.Child = this.mainWindow1;
            // 
            // checkBoxAlternativeRoute
            // 
            this.checkBoxAlternativeRoute.AutoSize = true;
            this.checkBoxAlternativeRoute.Location = new System.Drawing.Point(44, 189);
            this.checkBoxAlternativeRoute.Name = "checkBoxAlternativeRoute";
            this.checkBoxAlternativeRoute.Size = new System.Drawing.Size(194, 21);
            this.checkBoxAlternativeRoute.TabIndex = 22;
            this.checkBoxAlternativeRoute.Text = "Alternative Tourism Route";
            this.checkBoxAlternativeRoute.UseVisualStyleBackColor = true;
            // 
            // comboBoxChoiceTourism
            // 
            this.comboBoxChoiceTourism.FormattingEnabled = true;
            this.comboBoxChoiceTourism.Location = new System.Drawing.Point(352, 47);
            this.comboBoxChoiceTourism.Name = "comboBoxChoiceTourism";
            this.comboBoxChoiceTourism.Size = new System.Drawing.Size(77, 24);
            this.comboBoxChoiceTourism.TabIndex = 23;
            this.comboBoxChoiceTourism.SelectedIndexChanged += new System.EventHandler(this.comboBoxChoiceTourisme_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(260, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 17);
            this.label1.TabIndex = 24;
            this.label1.Text = "Tourisme";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxChoiceTourism);
            this.Controls.Add(this.checkBoxAlternativeRoute);
            this.Controls.Add(this.bingMapElement);
            this.Controls.Add(this.buttonAide);
            this.Controls.Add(this.labelAide);
            this.Controls.Add(this.textBoxDebug);
            this.Controls.Add(this.textBoxAdresseArrivee);
            this.Controls.Add(this.labelAdresseArrivee);
            this.Controls.Add(this.buttonCalcul);
            this.Controls.Add(this.textBoxAdresseDepart);
            this.Controls.Add(this.labelAdresseDepart);
            this.Controls.Add(this.labelInfos);
            this.Controls.Add(this.labelVille);
            this.Controls.Add(this.comboboxVille);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboboxVille;
        private System.Windows.Forms.Label labelVille;
        private System.Windows.Forms.Label labelInfos;
        private System.Windows.Forms.Label labelAdresseDepart;
        private System.Windows.Forms.TextBox textBoxAdresseDepart;
        private System.Windows.Forms.Button buttonCalcul;
        private System.Windows.Forms.TextBox textBoxAdresseArrivee;
        private System.Windows.Forms.Label labelAdresseArrivee;
        private System.Windows.Forms.TextBox textBoxDebug;
        private System.Windows.Forms.Label labelAide;
        private System.Windows.Forms.Button buttonAide;
        private System.Windows.Forms.Integration.ElementHost bingMapElement;
        private MainWindow mainWindow1;
        private System.Windows.Forms.CheckBox checkBoxAlternativeRoute;
        private System.Windows.Forms.ComboBox comboBoxChoiceTourism;
        private System.Windows.Forms.Label label1;
    }
}

