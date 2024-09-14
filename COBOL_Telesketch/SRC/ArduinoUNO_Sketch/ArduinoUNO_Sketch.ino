/*
 **********************************************************************
 * Name:    Teleskech.ino                                             *
 * - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  *
 * Created: 18.04.2024 10:01:49                                       *
 * Edit:    18.04.2024 10:01:49                                       *
 * Author:  Isaac Garcia Peveri                                       *
 *          isacco1975gp@gmail.com                                    *
 * - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  *
 **********************************************************************
*/

#pragma region "Working-Storage"
const int potY = A3;
const int potX = A5;

const int BURED = 6;
const int BUBLU = 5;
const int BUYLW = 4;
const int BUWHT = 3;
const int BURST = 7;

int xValue;
int yValue;

byte BUREDValue = 0;
byte BUBLUValue = 0;
byte BUYLWValue = 0;
byte BUWHTValue = 0;
byte BURSTValue = 0;

void setup()
{  
   Serial.begin(9600);

   pinMode(BURED, INPUT);
   pinMode(BUBLU, INPUT);
   pinMode(BUYLW, INPUT);
   pinMode(BUWHT, INPUT);
   pinMode(BURST, INPUT);
}

///
/// Reading Loop
///
void loop()
{
   xValue = analogRead(potY);
   yValue = analogRead(potX);

   char buff[4];
   int stringLength = 4;
   int decimalPrecision = 0;

   dtostrf(xValue, stringLength, decimalPrecision, buff);
   padZeros(buff);

   //clamp max size
   String xValuePadded;
   for(int idx = 0; idx < stringLength; idx++)
   {
      xValuePadded += buff[idx];
   }

   dtostrf(yValue, stringLength, decimalPrecision, buff);
   padZeros(buff);

   //clamp max size
   String yValuePadded;
   for(int idx = 0; idx < stringLength; idx++)
   {
      yValuePadded += buff[idx];
   }

   Serial.print(xValuePadded);
   Serial.print(";");
   Serial.print(yValuePadded);
   Serial.println("");
   
   BUREDValue = digitalRead(BURED);
   BUBLUValue = digitalRead(BUBLU);
   BUYLWValue = digitalRead(BUYLW);
   BUWHTValue = digitalRead(BUWHT);
   BURSTValue = digitalRead(BURST);

/*
   Serial.println(BUREDValue);
   Serial.println(BUBLUValue);
   Serial.println(BUYLWValue);
   Serial.println(BUWHTValue);
   Serial.println(BURSTValue);
*/
   if (1 == BUREDValue) 
   {
      Serial.println("RRRRRRRRR");
   }
   
   if (1 == BUBLUValue) 
   {
      Serial.println("BBBBBBBBB");    
   }

   if (1 == BUYLWValue) 
   {
      Serial.println("YYYYYYYYY");    
   }

   if (1 == BUWHTValue) 
   {
      Serial.println("WWWWWWWWW");    
   }

   if (1 == BURSTValue) 
   {
      Serial.println("CCCCCCCCC");
   }

   //Serial.println("");
   delay(15);
}

void padZeros(char* charStr) //this doesn't handle negative numbers
{
  for (int i = 0; i < strlen(charStr); i++)
  {
    if (charStr[i]==' ')
      charStr[i]='0';
    else
      break;
  }
}
