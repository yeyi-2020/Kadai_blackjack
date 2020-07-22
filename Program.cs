using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Kadai_blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("★☆★☆★☆★☆★☆ブラックジャックへようこそ！★☆★☆★☆★☆★☆");
            Console.WriteLine("ゲームを開始します。");

            ArrayList playingCards = Playingcards();

            for(int i=0; i < playingCards.Count; i++)
            {
                Console.WriteLine(playingCards[i]);
            }
            

            Player player = new Player();
            Dealer dealer = new Dealer();

            
            string cardName;

            
            cardName = RandomCard(playingCards);
            Console.WriteLine($"あなたの引いたカードは{cardName}です。");
            player.Hiku(CardPoint(cardName));
            playingCards.Remove(cardName);
            cardName = RandomCard(playingCards);
            Console.WriteLine($"あなたの引いたカードは{cardName}です。");
            player.Hiku(CardPoint(cardName));
            playingCards.Remove(cardName);



            cardName = RandomCard(playingCards);
            Console.WriteLine($"ディーナーの引いたカードは{cardName}です。");
            dealer.Hiku(CardPoint(cardName));
            playingCards.Remove(cardName);
            cardName = RandomCard(playingCards);
            Console.WriteLine("ディーナーの2枚目のカードは分かりません。");
            dealer.Hiku(CardPoint(cardName));
            dealer.SecondCard(cardName);
            playingCards.Remove(cardName);

            Console.WriteLine($"あなたの現在の得点は{player.getPoints()}。");

            

            while (player.points < 21 )
            {

                Console.WriteLine("カードを引きますか？引く場合はYを、引かない場合はNを入力してください。");
                string input = Console.ReadLine();

                if (!(input == "Y" || input == "N"))
                {
                    Console.WriteLine("引く場合はYを、引かない場合はNを入力してください。");
                    continue;
                }

                else if (input == "Y")
                {
                    cardName = RandomCard(playingCards);
                    Console.WriteLine($"あなたの引いたカードは{cardName}です。");
                    player.Hiku(CardPoint(cardName));
                    playingCards.Remove(cardName);
                    Console.WriteLine($"あなたの得点は{player.getPoints()}です。");
              
                    continue;

                }
                else 
                {
                    
                    break;
                }
            }

            if(player.getPoints() < 21)
            {
                while (dealer.getPoints() < 17)
                {

                    cardName = RandomCard(playingCards);
                    Console.WriteLine($"ディーナーの引いたカードは{cardName}です。");
                    dealer.Hiku(CardPoint(cardName));
                    playingCards.Remove(cardName);
                    continue;
                }
            }
            


            Console.WriteLine($"ディーナーの2枚目のカードは{dealer.getSecondCard()}です。");
            Console.WriteLine($"あなたの得点は{player.getPoints()}です。");
            Console.WriteLine($"ディーナーの得点は{dealer.getPoints()}です。");
            whoWin(player.getPoints(), dealer.getPoints());
            Console.WriteLine("★★★ブラックジャック終了！また遊んでね！★★★");






            /*           foreach (var card in playingCards)
                       {
                           Console.WriteLine(card);
                       }*/
        }


        static ArrayList Playingcards()
        {
            ArrayList playingCards = new ArrayList();
            string[] cardSets = { "diamond", "club", "heart", "spade" };

            for (int i = 1; i <= 13; i++)
            {

                for (int j = 0; j < 4; j++)
                {
                    string card;
                    switch (i)
                    {
                        case 1:
                            card = cardSets[j] + " A";
                            playingCards.Add(card);
                            break;
                        
                        case 11:
                            card = cardSets[j] + " J";
                            playingCards.Add(card);
                            break;

                        case 12:
                            card = cardSets[j] + " Q";
                            playingCards.Add(card);
                            break;

                        case 13:
                            card = cardSets[j] + " K";
                            playingCards.Add(card);
                            break;

                        default:
                            card = cardSets[j] + " " + i.ToString();
                            playingCards.Add(card);
                            break;

                    }
                    
                }
            }
            return playingCards;
        }

        static int CardPoint(string cardName)
        {
            int numberPos = cardName.IndexOf(" ") +1;
            string pips = cardName.Substring(numberPos);
            int cardPoint;
            if (pips == "A")
            {
                cardPoint = 1;
            } else if (pips == "J" || pips == "Q" || pips == "K")
            {
                cardPoint = 10;
            } else
            {
                cardPoint = Convert.ToInt32(pips);
            }
            
            return cardPoint;
        }

        static string RandomCard(ArrayList playingCards)
        {
            Random rnd = new Random();
            int cardIndex = rnd.Next(playingCards.Count);
            string cardName = (string)playingCards[cardIndex];
            return cardName;
        }

        static void whoWin(int player, int dealer)
        {
            int playerDiff = Math.Abs(21 - player);
            int dealerDiff = Math.Abs(21 - dealer);

            if(playerDiff < dealerDiff)
            {
                Console.WriteLine("★☆★☆★☆★☆★☆あなたの勝ちです！★☆★☆★☆★☆★☆");
            } else
            {
                Console.WriteLine("====================あなたの負けです！====================");
            }
        }

    }

 
    class Player
    {
        public int points;

        public void Hiku(int card)
        {
            points += card;
        }

        public int getPoints()
        {
            return points;
        }
    }

    class Dealer
    {
        public string secondCard;
        public int points;

        public void Hiku(int card)
        {
            points += card;
        }

        public void SecondCard(string secondCard)
        {
            this.secondCard = secondCard;
        }

        public int getPoints()
        {
            return points;
        }

        public string getSecondCard()
        {
            return secondCard;
        }
    }

}
