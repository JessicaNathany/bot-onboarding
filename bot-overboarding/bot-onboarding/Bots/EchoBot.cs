using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace bot_onboarding.Bots
{
    public class EchoBot : ActivityHandler
    {
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var inputUsuario = turnContext.Activity.Text;

            var voltar = turnContext.Activity.Text;

            if (voltar != null && voltar.ToUpper() == "V")
            {
                InfoUsuario.Time = null;
                InfoUsuario.Funcao = null;
                InfoUsuario.Nome = null;
            }

            if (InfoUsuario.Nome == null && inputUsuario.Length > 1)
            {
                InfoUsuario.Nome = turnContext.Activity.Text;
                InfoUsuario.Mensagem = $"Olá, {InfoUsuario.Nome}! Qual seu time?";
                
                await turnContext.SendActivityAsync(MessageFactory.Text(InfoUsuario.Mensagem, InfoUsuario.Mensagem), cancellationToken);
                return;
            }
            else if(InfoUsuario.Nome == null && inputUsuario.Length <= 1 && !inputUsuario.Equals("v", StringComparison.InvariantCultureIgnoreCase))
            {
                InfoUsuario.Mensagem = $"Por gentileza, informe um nome válido.";

                await turnContext.SendActivityAsync(MessageFactory.Text(InfoUsuario.Mensagem, InfoUsuario.Mensagem), cancellationToken);
                return;
            }
            else if(InfoUsuario.Nome == null)
            {
                var welcomeText = "Olá seja bem-vindo! Qual seu nome?";
                await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
            }

            if (InfoUsuario.Time == null && InfoUsuario.Nome != null)
            {
                InfoUsuario.Time = turnContext.Activity.Text;
                InfoUsuario.Mensagem = $"Legal {InfoUsuario.Nome}, você vai amar o time **{InfoUsuario.Time}** !";

                await turnContext.SendActivityAsync(MessageFactory.Text(InfoUsuario.Mensagem, InfoUsuario.Mensagem), cancellationToken);

                var selecioneOpcoesTime = "**Digite o número da sua função no time** : \r\n  1- Desenvolvedor \r\n 2- UX \r\n 3- QA \r\n 4- SM \r\n 5- PO";
                await turnContext.SendActivityAsync(MessageFactory.Text(selecioneOpcoesTime, selecioneOpcoesTime), cancellationToken);
                return;
            }

            if(InfoUsuario.TipoPapel == 0)
            {
                var selecioneOpcoesDev = "**Essas são os acessos que você precisa. Digite uma opção que deseja ter acesso ou a letra V para voltar ao inicio:**" +
                                          "\r\n 1- Jira ou Confluence \r\n 2- Gitlab \r\n 3- Banco de Dados \r\n 4- VPN \r\n 5- AWS \r\n 6- Grupo Teams \r\n 7- Como acessar o GSenha  \r\n 8- Requisitos mínimos para montar o ambiente \r\n 'V'- Voltar ao inicio";

                InfoUsuario.TipoPapel = (TipoPapel)Enum.Parse(typeof(TipoPapel), turnContext.Activity.Text, true);

                await turnContext.SendActivityAsync(MessageFactory.Text(selecioneOpcoesDev, selecioneOpcoesDev), cancellationToken);
                return;
            }

            if (InfoUsuario.TipoAcesso == 0)
            {
                var selecioneOpcoesAcesso = string.Empty;

                InfoUsuario.TipoAcesso = (Acessos)Enum.Parse(typeof(Acessos), turnContext.Activity.Text, true);

                if(InfoUsuario.TipoAcesso == Acessos.AWS)
                {
                    selecioneOpcoesAcesso  = "**Para solicitar o acesso a AWS, siga caminho abaixo:** \r\n" +
                                         " Acesse o (Acesso Globo) no seguinte link: https://acesso.g.globo/accessRequest/accessRequest.jsf#/accessRequest \r\n" +
                                         "1º Selecione a opção Solicitar/Remover Acessos \r\n" +
                                         "2º No campo de busca, digite AWS e selecione o perfil e o time no qual você pertence, exemplo: \r\n" +
                                         "AWS | JCORP - PRODUCAO ESTUDIOS | DEV - 874081334926 - GMUSIC \r\n" +
                                         "3º Após selecionar o perfil, clique em next e depois em Submit \r\n" +
                                         "Digite a letra **V** para voltar ao inicio";
                }

                if (InfoUsuario.TipoAcesso == Acessos.Gitlab)
                {
                    selecioneOpcoesAcesso = "**Para solicitar o acesso a Gitlab, siga caminho abaixo:** \r\n" +
                                         " Acesse o (Acesso Globo) no seguinte link: https://acesso.g.globo/accessRequest/accessRequest.jsf#/accessRequest \r\n" +
                                         "1º Selecione a opção Solicitar/Remover Acessos e digite no campo de busca Gitlab \r\n" +
                                         "2º No campo de busca, digite gitlab e selecione o perfil e o time no qual você pertence, exemplo: \r\n" +
                                         "Ex: GITLAB | TECNOLOGIA - HDG - JCORP - PRODUCAO DE CONTEUDO \r\n" +
                                         "3º Após selecionar o perfil, clique em next e depois em Submit \r\n" +
                                         "Digite a letra **V** para voltar ao inicio";
                }

                if (InfoUsuario.TipoAcesso == Acessos.GSenha) 
                {

                    selecioneOpcoesAcesso = "**Para poder acesasr o Gsenha, siga caminho abaixo:** \r\n" +
                                            "1º Certifique-se que você esteja conectado na VPN \r\n" +
                                            "2º Certifique-se que tenha acesso ao Gsenha, caso contrário, solicite a SM ou coordenador \r\n" +
                                            "3º Clique no link: https://gsenha.globoi.com/login?next=/passwords \r\n" +
                                            "4º Clique no botão Login Backstage e depois clicar Entrar com Backstage \r\n" +
                                            "Digite a letra **V** para voltar ao inicio";
                }


                if (InfoUsuario.TipoAcesso == Acessos.BancoDados)
                {

                    selecioneOpcoesAcesso = "**Para solicitar o acesso ao Banco de Dados, siga caminho abaixo:** \r\n" +
                                            "Se você tiver acesso ao Confluence, então clique no link e siga o passo a passo de instalação e configuração https://globo.atlassian.net/wiki/spaces/ENCROS/pages/403526877408/Globo+CLI+Instala+o \r\n" +
                                            "Se não, contacte seu SM ou coordenador do seu time. \r\n" +
                                            "Digite a letra **V** para voltar ao inicio";
                }                                    

                if (InfoUsuario.TipoAcesso == Acessos.JiraCoFluence)
                {
                    selecioneOpcoesAcesso = "**Para conseguir o acesso ao Jira ou Confluence** \r\n" +
                                            "procure a SM ou coordenador do seu time! \r\n" +
                                            "Digite a letra **V** para voltar ao inicio";
                }

                if (InfoUsuario.TipoAcesso == Acessos.GruposTeams)
                {
                    selecioneOpcoesAcesso = "**Para conseguir o acesso aos grupos do Teams** \r\n" +
                                            "procure a SM ou coordenador do seu time! \r\n" +
                                            "Digite a letra **V** para voltar ao inicio";
                }

                if (InfoUsuario.TipoAcesso == Acessos.VPN)
                {
                    selecioneOpcoesAcesso = "**Para conseguir o acesso a VPN, acesse o link:** \r\n" +
                                            "Se você tiver acesso ao Confluence, então clique no link https://globo.atlassian.net/wiki/spaces/JCEDC/pages/403448136247/JCEC+Guia+para+inclus+o+de+novos+colaboradores \r\n" +
                                            "Se não, contacte seu SM ou coordenador do seu time. \r\n" +
                                            "Vá direto na opção (Solicitação de acesso a VPN) \r\n" +
                                            "Digite a letra **V** para voltar ao inicio";
                }

                if (InfoUsuario.TipoAcesso == Acessos.RequisitosMinimosAmbiente)
                {
                    selecioneOpcoesAcesso = "**Veja abaixo os requisitos mínimos para montar seu ambiente.** \r\n" +
                                            "1º Instale o Visual Studio Professional 2019 para projetos legados, e Visual Studio Professional 2022 ou VSCode para projetos novos (InteliJ para proejtos Java) \r\n" +
                                            "2º Instale o MySQL Worbench ou Dbeaver \r\n" +
                                            "3º Git \r\n" +
                                            "4º Configurar o tunnel SSH com os endpoints dos ambientes \r\n" +
                                            "5º Postman ou Insomnia \r\n" +
                                            "Digite a letra **V** para voltar ao inicio";
                }

                await turnContext.SendActivityAsync(MessageFactory.Text(selecioneOpcoesAcesso, selecioneOpcoesAcesso), cancellationToken);
                return;
            }
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Olá seja bem-vindo! Qual seu nome?";

            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }
    }
}
