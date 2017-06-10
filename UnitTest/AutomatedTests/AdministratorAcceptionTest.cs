﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests;
using Xamarin.UITest;

namespace UnitTest.AutomatedTests {
    class AdministratorAcceptionTest {

        [TestFixture(Platform.Android)]
        [TestFixture(Platform.iOS)]
        public class Tests {
            IApp app;
            Platform platform;

            public Tests(Platform platform) {
                this.platform = platform;

            }

            [SetUp]
            public void BeforeEachTest() {
                app = AppInitializer.StartApp(platform);

                // Opening Administrator account
                app.EnterText("EtRegistrationLoginPage", "12345678");
                app.EnterText("EtPasswordLoginPage", "Pb1234567");
                app.Tap("BtLoginLoginPage");
                app.WaitForNoElement("BtLoginLoginPage");
            }



            [Test]
            public void CreateNewForum() {
                app.Tap("ButtonPlusAppMasterPageDetail");
                app.Tap("ButtonNewForumAppMasterPageDetail");

                app.EnterText("EtTitleNewForumPage", "Forum Teste");
                app.EnterText("EtPlaceNewForumPage", "Local Teste");
                app.EnterText("EdScheduleNewForumPage", "Pauta Teste. 123.");
                app.DismissKeyboard();
                app.ScrollDown();
                app.Tap("ButtonCriarForumPageNewForunsPage");
                app.WaitForElement("alertTitle");

                //app.Repl();
                Assert.IsNotNull(app.Query("OK"));

            }
            [Test]
            public void UserRegistration() {
                app.Tap(e => e.Marked("ButtonPlusAppMasterPageDetail"));
                app.Tap(e => e.Marked("ButtonNewCoordenadorAppMasterPageDetail"));
                app.EnterText("LabelNomeCompletoUserRegistrationPage", "Nome Teste");
                app.EnterText("LabelEmailUserRegistrationPage", "teste@email.com");
                app.EnterText("LabelMatriculaUserRegistrationPage", "246813579");
                app.EnterText("LabelSenhaUserRegistrationPage", "Pb12345678");
                app.DismissKeyboard();
                app.ScrollDown();
                app.Tap("PickerCursoUserRegistrationPage");
                app.Tap("Engenharias");
                app.Tap(e => e.Marked("ButtonConfirmarUserRegistrationPage"));
                app.Tap(e => e.Marked("button1"));
                Assert.IsNotNull("Registrar novo usuário");
            }

            [Test]
            public void ShowUser() {
                app.Tap(e => e.Marked("ButtonUserButtonAppMasterPageDetail"));
                app.WaitForElement("Ver detalhes");
                app.Tap(e => e.Marked("Nome Teste"));
                Assert.IsNotNull("LabelRemoverUsuarioUserDetailPage");
            }

            [Test]
            public void RemoveUser() {
                app.Tap(e => e.Marked("ButtonUserButtonAppMasterPageDetail"));
                app.WaitForElement("Ver detalhes");
                app.Tap(e => e.Marked("Nome Teste"));
                app.Tap(e => e.Marked("LabelRemoverUsuarioUserDetailPage"));
                app.Tap(e => e.Marked("button1"));
                Assert.IsNotNull("Ver detalhes");
            }

            [Test]
            public void RemoveForum() {
                app.Tap("ButtonForunsAppMasterPageDetail");
                app.Tap(c => c.Marked("Ver detalhes"));
                app.Tap("ButtonDeletarForumForumDetailPage");
                app.Tap(c => c.Marked("Sim"));
                app.WaitForNoElement("Sim");
                Assert.IsNotNull("Fóruns");
            }

            [Test]
            public void ListsFormsForAdministrators() {
                app.Tap("Formulários");
                Assert.IsNotNull(app.Query("Ver detalhes"));
            }
        }
    }
}