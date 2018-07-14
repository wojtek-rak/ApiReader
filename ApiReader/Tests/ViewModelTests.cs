using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiReader.Tests
{
    [TestFixture]
    class ViewModelTests
    {
        [Test]
        public void CanCreateAndShowWpfWindow()
        {
            CrossThreadTestRunner runner = new CrossThreadTestRunner();
            runner.RunInSTA(
              delegate
              {
                  MainWindow window = new MainWindow();
                  window.Show();
              });
        }

        [Test]
        public void IntervalErrorTextBoxInitTest()
        {
            CrossThreadTestRunner runner = new CrossThreadTestRunner();
            runner.RunInSTA(
              delegate
              {
                  MainWindow window = new MainWindow();
                  Assert.AreEqual(String.Empty, window.apiText.Text);
              });
        }

        [Test]
        public void IntervalErrorTextBoxFillTest()
        {
            CrossThreadTestRunner runner = new CrossThreadTestRunner();
            runner.RunInSTA(
              delegate
              {
                  MainWindow window = new MainWindow();
                  window.intervalBox.Text = "1";
                  Assert.AreEqual(String.Empty, window.apiText.Text);
              });
        }

        [Test]
        public void IntervalTextBoxParseTest()
        {
            CrossThreadTestRunner runner = new CrossThreadTestRunner();
            runner.RunInSTA(
              delegate
              {
                  MainWindow window = new MainWindow();
                  window.intervalBox.Text = "12";
                  Assert.AreEqual(12, window.Interval);
              });
        }
    }
}
