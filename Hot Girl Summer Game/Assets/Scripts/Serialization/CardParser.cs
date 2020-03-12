using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class CardParser
{

    public static Dictionary<string, Card.CardInfo> ReadMasterSpreadsheet(TextAsset spreadsheet)
    {
        Dictionary<string, Card.CardInfo> dictionaryOfCards = new Dictionary<string, Card.CardInfo>();
        var stringToRead = new StringReader(spreadsheet.text);
        stringToRead.ReadLine();
        char[] charsToTrim = { '(', '\"', ')' };
        while (stringToRead.Peek() != -1)
        {
            //stringToRead.ReadLine();
            var unparsedCardInfo = stringToRead.ReadLine().Split(',');
            var calmVP = unparsedCardInfo[7].Trim(charsToTrim);
            var totalVP = unparsedCardInfo[10].Trim(charsToTrim);
            var finalCardInfo = new Card.CardInfo();
            //dictionaryOfCards.Add(unparsedCardInfo[0], new Card.CardInfo());
            finalCardInfo.cardName = unparsedCardInfo[0]; //set name
            finalCardInfo.type = Card.Parse(unparsedCardInfo[1]); //set type
            finalCardInfo.value = int.Parse(unparsedCardInfo[2]); // set value
            finalCardInfo.isPlayable = bool.Parse(unparsedCardInfo[3]); // set isPlayable
            finalCardInfo.text = unparsedCardInfo[4]; // set text ***Does not take commas into account


            finalCardInfo.normalArt = Resources.Load<Sprite>("Cards_/" + unparsedCardInfo[5]);
            finalCardInfo.hoverArt = Resources.Load<Sprite>("Cards_/" + unparsedCardInfo[6]);//set art

            finalCardInfo.buyCost = new Card.VictoryPoints();
            finalCardInfo.buyCost.calmPoints = int.Parse(calmVP);
            finalCardInfo.buyCost.bubblyPoints = int.Parse(unparsedCardInfo[8]);
            finalCardInfo.buyCost.hypePoints = int.Parse(unparsedCardInfo[9]);
            finalCardInfo.buyCost.totalPoints = int.Parse(totalVP); //set buyCost

            var dictionarykey = finalCardInfo.cardName;
            if (finalCardInfo.cardName.Split(' ').Length > 1)
            {
                dictionarykey = finalCardInfo.cardName.Split(' ')[0] + finalCardInfo.cardName.Split(' ')[1];
            }

            dictionaryOfCards.Add(dictionarykey, finalCardInfo); // add to dictionary

            if (stringToRead.Peek() == -1) break;
        }

        stringToRead.Close();
        return dictionaryOfCards;


    }
}
