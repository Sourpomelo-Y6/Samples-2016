﻿using System;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Windows.Controls;
using System.Windows.Data;

namespace BindingConsole
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Bind_OneWay();
            Bind_TwoWay();
            Bind_Indexer_TwoWay();
            Bind_Expando_TwoWay();
            Bind_Collection();
        }

        static void Bind_OneWay()
        {
            // Binding Source (Any object).
            var person = new Person1 { Id = 123, Name = "Taro" };

            // Binding Target must be FrameworkElement.
            var textBlock = new TextBlock { Text = "Default" };
            Console.WriteLine(textBlock.Text);

            // Binds target to source.
            var binding = new Binding(nameof(person.Name)) { Source = person };
            textBlock.SetBinding(TextBlock.TextProperty, binding);
            Console.WriteLine(textBlock.Text);

            // Changes source value.
            person.Name = "Jiro";
            Console.WriteLine(textBlock.Text);
        }

        static void Bind_TwoWay()
        {
            // Binding Source (Any object).
            var person = new Person1 { Id = 123, Name = "Taro" };

            // Binding Target must be FrameworkElement.
            var textBox = new TextBox { Text = "Default" };
            Console.WriteLine(textBox.Text);

            // Binds target to source.
            var binding = new Binding(nameof(person.Name)) { Source = person, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            textBox.SetBinding(TextBox.TextProperty, binding);
            Console.WriteLine(textBox.Text);

            // Changes source value.
            person.Name = "Jiro";
            Console.WriteLine(textBox.Text);

            // Changes target value.
            textBox.Text = "Saburo";
            Console.WriteLine(person.Name);
        }

        static void Bind_Indexer_TwoWay()
        {
            // Binding Source with indexer.
            var map = new PersonMap { [123] = "Taro" };

            // Binding Target must be FrameworkElement.
            var textBox = new TextBox { Text = "Default" };
            Console.WriteLine(textBox.Text);

            // Binds target to source.
            var binding = new Binding("[123]") { Source = map, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            textBox.SetBinding(TextBox.TextProperty, binding);
            Console.WriteLine(textBox.Text);

            // Changes source value.
            map[123] = "Jiro";
            Console.WriteLine(textBox.Text);

            // Changes target value.
            textBox.Text = "Saburo";
            Console.WriteLine(map[123]);
        }

        static void Bind_Expando_TwoWay()
        {
            // Binding Source (Any object).
            dynamic person = new ExpandoObject();
            person.Id = 123;
            person.Name = "Taro";

            // Binding Target must be FrameworkElement.
            var textBox = new TextBox { Text = "Default" };
            Console.WriteLine(textBox.Text);

            // Binds target to source.
            var binding = new Binding(nameof(person.Name)) { Source = person, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            textBox.SetBinding(TextBox.TextProperty, binding);
            Console.WriteLine(textBox.Text);

            // Changes source value.
            person.Name = "Jiro";
            Console.WriteLine(textBox.Text);

            // Changes target value.
            textBox.Text = "Saburo";
            Console.WriteLine(person.Name);
        }

        static void Bind_Collection()
        {
            var taro = new Person1 { Id = 123, Name = "Taro" };
            var jiro = new Person1 { Id = 234, Name = "Jiro" };

            // Binding Source (collection).
            var people = new ObservableCollection<Person1> { taro };

            // Binding Target.
            var itemsControl = new ItemsControl();
            Console.WriteLine(itemsControl.Items.Count);

            // Binds target to source.
            // MEMO: Binding Source のオブジェクト自体が変更されないのであれば、
            // ItemsSource プロパティのデータ バインディングは必須ではありません。
            itemsControl.ItemsSource = people;
            Console.WriteLine(itemsControl.Items.Count);

            // Changes source collection.
            people.Add(jiro);
            Console.WriteLine(itemsControl.Items.Count);
            people.RemoveAt(0);
            Console.WriteLine(itemsControl.Items.Count);

            // MEMO: ItemsSource に値を設定している場合、Items を直接変更しようとすると例外が発生します。
            //itemsControl.Items.Add(jiro);
        }
    }
}
