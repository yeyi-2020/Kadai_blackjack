using System;
using System.Collections;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("★☆★☆★☆★☆★☆ブラックジャックへようこそ！★☆★☆★☆★☆★☆");
            Console.WriteLine("ゲームを開始します。");
            Console.WriteLine();

            Player player = new Player();
            Dealer dealer = new Dealer();

            ArrayList playingCards = Playingcards();

            string cardName;

            for(int i =1; i <= 2; i++)
            {
                cardName = RandomCard(playingCards); //ランダムにカードを引く
                Console.WriteLine($"あなたの引いたカードは{cardName}です。"); //出力する
                Console.WriteLine();
                player.Hiku(CardPoint(cardName)); //カードを引く
                playingCards.Remove(cardName); //引く際にガードの重複はないようにする
            }
           
            cardName = RandomCard(playingCards);
            Console.WriteLine($"ディーナーの引いたカードは{cardName}です。");
            Console.WriteLine();
            dealer.Hiku(CardPoint(cardName));
            playingCards.Remove(cardName);
            cardName = RandomCard(playingCards);

            Console.WriteLine("ディーナーの2枚目のカードは分かりません。");
            Console.WriteLine();
            dealer.Hiku(CardPoint(cardName));
            dealer.SecondCard(cardName);
            playingCards.Remove(cardName);

            Console.WriteLine($"あなたの現在の得点は{player.GetPoints()}。");

            while (player.points < 21) //プレイヤー21点まで引くか引かないかを聞く
            {

                Console.WriteLine("カードを引きますか？引く場合はYを、引かない場合はNを入力してください。");
                string input = Console.ReadLine();

                if (!(input == "Y" || input == "N"))
                {
                    Console.WriteLine("引く場合はYを、引かない場合はNを入力してください。");
                    
                }

                else if (input == "Y")
                {
                    cardName = RandomCard(playingCards);
                    Console.WriteLine($"あなたの引いたカードは{cardName}です。");
                    player.Hiku(CardPoint(cardName));
                    playingCards.Remove(cardName);
                    Console.WriteLine($"あなたの得点は{player.GetPoints()}です。");
                    Console.WriteLine();

                }

                else //引かない場合
                {
                    break;
                }

            }

            if (player.GetPoints() < 21) //プレイヤーの点数が21より小さい、ディーラーがカードを引く
            {
                while (dealer.GetPoints() < 17) //ディーラーが17点まで引き続ける
                {

                    cardName = RandomCard(playingCards);
                    Console.WriteLine($"ディーナーの引いたカードは{cardName}です。");
                    dealer.Hiku(CardPoint(cardName));
                    playingCards.Remove(cardName);
                }

            }


            Console.WriteLine();
            Console.WriteLine($"ディーナーの2枚目のカードは{dealer.GetSecondCard()}です。");
            Console.WriteLine($"あなたの得点は{player.GetPoints()}です。");
            Console.WriteLine($"ディーナーの得点は{dealer.GetPoints()}です。");
            Console.WriteLine();
            Console.WriteLine("★☆★☆★☆★☆★☆★結果★☆★☆★☆★☆★☆★");
            WhoWin(player.GetPoints(), dealer.GetPoints());
            Console.WriteLine("★☆★☆★☆★☆★☆★☆★☆★☆★☆★☆★☆★☆");
            Console.WriteLine("★★★ブラックジャック終了！また遊んでね！★★★");
        }
        
       
        static ArrayList Playingcards() //52枚トランプ
        {
            ArrayList playingCards = new ArrayList();
            string[] cardSets = { "ダイヤ", "クラブ", "ハート", "スペード" }; //"diamond", "club", "heart", "spade"

            for (int i = 1; i <= 13; i++) //AからKまでのカード
            {

                for (int j = 0; j < 4; j++) //スート
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

        static int CardPoint(string cardName) //トランプの点数
        {
            int numberPos = cardName.IndexOf(" ") + 1;
            string pips = cardName.Substring(numberPos);
            int cardPoint;
            if (pips == "A")
            {
                cardPoint = 1;
            }

            else if (pips == "J" || pips == "Q" || pips == "K")
            {
                cardPoint = 10;
            }

            else
            {
                cardPoint = Convert.ToInt32(pips);
            }

            return cardPoint;
        }

        static string RandomCard(ArrayList playingCards) //ランダムにカードを引く
        {
            Random rnd = new Random();
            int cardIndex = rnd.Next(playingCards.Count);
            string cardName = (string)playingCards[cardIndex];
            return cardName;
        }

        static void WhoWin(int player, int dealer) //結果を判断して、出力する
        {
            int playerDiff = Math.Abs(21 - player);
            int dealerDiff = Math.Abs(21 - dealer);

            if (playerDiff < dealerDiff)
            {
                Console.WriteLine();
                Console.WriteLine("*☆,°*:.☆*☆,°*:.☆【あなたの勝ちです！】*☆,°*:.☆*☆,°*:.☆");
                Console.WriteLine();
            }

            else
            {
                Console.WriteLine();
                Console.WriteLine("=============【あなたの負けです！】=============");
                Console.WriteLine();
            }

        }

    }


    class Player //プレイヤーの点数
    {
        public int points;

        public void Hiku(int card) //カードを引く時、点数を合計する
        {
            points += card;
        }

        public int GetPoints()
        {
            return points;
        }

    }

    class Dealer //ディーラーの点数
    {
        public string secondCard;
        public int points;

        public void Hiku(int card) //カードを引く時、点数を合わせる
        {
            points += card;
        }

        public void SecondCard(string secondCard) //二枚目のカードを記録する
        {
            this.secondCard = secondCard;
        }

        public int GetPoints()
        {
            return points;
        }

        public string GetSecondCard()
        {
            return secondCard;
        }

    }

}

