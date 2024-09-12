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
int xValue;
int yValue;

void setup()
{  
   Serial.begin(9600);
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
   Serial.println();
   //Serial.println(yValue);
   //tone(bu1, yValue + 400);

   delay(70);
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
