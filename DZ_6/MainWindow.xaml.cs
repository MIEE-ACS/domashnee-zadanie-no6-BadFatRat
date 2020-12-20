using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DZ_6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public abstract class Body
        {
            public class BodyException : Exception
            {
                public BodyException(string message)
                    : base(message)
                { }
            }
            virtual public double SurfaceArea() { return 0; } // площадь поверхности
            virtual public double Volume() { return 0; } // объём
            public override string ToString()
            {
                return $"\nПлощадь поверхности: {SurfaceArea():0.###}\nОбъём: {Volume():0.###}";
            }
        }

        public partial class Cube : Body
        {
            private double m_edge; // длина ребра
            public Cube()
            {
                Random rnd = new Random();
                m_edge = rnd.Next(1, 21) + rnd.NextDouble();
            }
            public Cube(double edge)
            {
                if (edge <= 0)
                    throw new BodyException("BelowZeroError");
                m_edge = edge;
            }
            public double Edge
            {
                get
                {
                    return m_edge;
                }
                set
                {
                    if (value <= 0)
                        throw new BodyException("BelowZeroError");
                    m_edge = value;
                }
            }
            public override double SurfaceArea()
            { 
                return Math.Pow(m_edge, 2) * 6;
            }
            public override double Volume()
            {
                return Math.Pow(m_edge, 3);
            }
            public override string ToString()
            {
                return $"Куб:\nДлина ребра: {m_edge:0.###}" + base.ToString();
            }
            public override bool Equals(object obj)
            {
                if (obj == null)
                    return false;
                Cube c = obj as Cube;
                if (c as Cube == null)
                    return false;
                return c.m_edge == this.m_edge;
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        public partial class Sphere : Body
        {
            private double m_radius;
            public Sphere()
            {
                Random rnd = new Random();
                m_radius = rnd.Next(1, 21) + rnd.NextDouble();
            }
            public Sphere(double radius)
            {
                if (radius <= 0)
                    throw new BodyException("BelowZeroError");
                m_radius = radius;
            }
            public double Radius
            {
                get
                {
                    return m_radius;
                }
                set
                {
                    if (value <= 0)
                        throw new BodyException("BelowZeroError");
                    m_radius = value;
                }
            }
            public override double SurfaceArea()
            {
                return 4 * Math.PI * Math.Pow(m_radius, 2);
            }
            public override double Volume()
            {
                return 4.0 / 3.0 * Math.PI * Math.Pow(m_radius, 3);
            }
            public override string ToString()
            {
                return $"Шар:\nРадиус: {m_radius:0.###}" + base.ToString();
            }
            public override bool Equals(object obj)
            {
                if (obj == null)
                    return false;
                Sphere c = obj as Sphere;
                if (c as Sphere == null)
                    return false;
                return c.m_radius == this.m_radius;
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        public partial class Cone : Body
        {
            public enum Type
            {
                RH, // радиус и высота
                RL, // радиус и образующая
                HL  // высота и образующая
            }
            private double m_radius; // радиус основания
            private double m_height; // высота
            private double m_generatrix; // образующая конуса  
            private Type m_type;
            // радиус || радиус || высота, высота || образующая || образующая, RH || RL || HL
            public Cone()
            {
                Random rnd = new Random();
                m_radius = rnd.Next(1, 21) + rnd.NextDouble();
                m_height = rnd.Next(1, 21) + rnd.NextDouble();
                m_generatrix = Math.Sqrt(Math.Pow(m_radius, 2) + Math.Pow(m_height, 2));
            }
            public Cone(double RRH, double HLL, Type type) 
            {
                if (RRH <= 0 || HLL <= 0)
                    throw new Exception("BelowZeroError");
                m_type = type;
                if (m_type == Type.RH)
                {
                    m_radius = RRH;
                    m_height = HLL;
                    m_generatrix = Math.Sqrt(HLL * HLL + RRH * RRH);
                }
                else if (m_type == Type.RL)
                {
                    m_radius = RRH;
                    m_height = Math.Sqrt(HLL * HLL - RRH * RRH);
                    m_generatrix = HLL;
                }
                else if (m_type == Type.HL)
                {
                    m_radius = Math.Sqrt(HLL * HLL - RRH * RRH);
                    m_height = RRH;
                    m_generatrix = HLL;
                }
            }
            public double Radius
            {
                get
                {
                    return m_radius;
                }
                set
                {
                    if (value <= 0)
                        throw new Exception("BelowZeroError");
                    m_radius = value;
                }
            }
            public double Height
            {
                get
                {
                    return m_height;
                }
                set
                {
                    if (value <= 0)
                        throw new Exception("BelowZeroError");
                    m_height = value;
                }
            }
            public double Generatrix
            {
                get
                {
                    return m_generatrix;
                }
                set
                {
                    if (value <= 0)
                        throw new Exception("BelowZeroError");
                    m_generatrix = value;
                }
            }
            public override double SurfaceArea()
            {
                return Math.PI * m_radius * (m_radius + m_generatrix);
            }
            public override double Volume()
            {
                return 1.0 / 3.0 * Math.PI * Math.Pow(m_radius, 2) * m_height;
            }
            public override string ToString()
            {
                return $"Конус:\nРадиус: {m_radius:0.###}\nВысота: {m_height:0.###}\n" +
                    $"Основание конуса:{m_generatrix:0.###}" + base.ToString();
            }
            public override bool Equals(object obj)
            {
                if (obj == null)
                    return false;
                Cone c = obj as Cone;
                if (c as Cone == null)
                    return false;
                return c.m_radius == this.m_radius && c.m_height == this.m_height && c.m_generatrix == this.m_generatrix;
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        List<Body> Bodies = new List<Body>
        {
            new Cube(), new Cube(), new Cube(),
            new Sphere(), new Sphere(), new Sphere(),
            new Cone(), new Cone(), new Cone()
        };

        public MainWindow()
        {
            InitializeComponent();
            UpdateBodiesList();
        }

        public void UpdateBodiesList()
        {
            lbBodies.Items.Clear();
            foreach (var body in Bodies)
            {
                lbBodies.Items.Add(body);
            }
        }

        private void bttnAddCube_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Bodies.Add(new Cube(double.Parse(tbEdge.Text)));
            }
            catch(FormatException)
            {
                MessageBox.Show("Проверьте введенные значения", "Ошибка данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Body.BodyException)
            {
                MessageBox.Show("Вводимые значения должны быть больше нуля", "Ошибка данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Обратитесь к разработчику: " + ex.Message, "Неизвестная ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                UpdateBodiesList();
            }
        }

        private void bttnAddSphere_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Bodies.Add(new Sphere(double.Parse(tbRadius.Text)));
            }
            catch (FormatException)
            {
                MessageBox.Show("Проверьте введенные значения", "Ошибка данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Body.BodyException)
            {
                MessageBox.Show("Вводимые значения должны быть больше нуля", "Ошибка данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Обратитесь к разработчику: " + ex.Message, "Неизвестная ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                UpdateBodiesList();
            }
        }

        private void bttnAddCone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(cbType.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите способ задания конуса", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }    
                else if (cbType.SelectedIndex == 0)
                {
                    Bodies.Add(new Cone(double.Parse(tbRadius.Text), double.Parse(tbHeight.Text), Cone.Type.RH));
                }
                else if (cbType.SelectedIndex == 1)
                {
                    Bodies.Add(new Cone(double.Parse(tbRadius.Text), double.Parse(tbGeneratrix.Text), Cone.Type.RL));
                }
                else if (cbType.SelectedIndex == 2)
                {
                    Bodies.Add(new Cone(double.Parse(tbHeight.Text), double.Parse(tbGeneratrix.Text), Cone.Type.RL));
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Проверьте введенные значения", "Ошибка данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Body.BodyException)
            {
                MessageBox.Show("Вводимые значения должны быть больше нуля", "Ошибка данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Обратитесь к разработчику: " + ex.Message, "Неизвестная ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                UpdateBodiesList();
            }
        }

        private void bttnDeleteChosen_Click(object sender, RoutedEventArgs e)
        {
            if (lbBodies.Items.Count == 0)
            {
                MessageBox.Show("В списке нет значений", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (lbBodies.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите элемент, который хотите удалить", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    Bodies.RemoveAt(lbBodies.SelectedIndex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Обратитесь к разработчику: " + ex.Message, "Неизвестная ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    UpdateBodiesList();
                }
            }                
        }
    }
}
