String serialValue = "";
String serialCmd = "";
String numSecs = "";

void setup() {
  pinMode(13, OUTPUT);
  //pinMode(myOtherDevicePIN, OUTPUT);
  //... and so on
  
  Serial.begin(9600); 
}

void loop() {
  if (Serial.available()) 
  {
    // read the incoming message: in string format like "X XX"
    serialValue = Serial.readString();
    serialCmd = serialValue.substring(0, 1); //Command
    numSecs = serialValue.substring(2,4);    //Time in Seconds

    if (serialCmd == "1") 
    {
      digitalWrite(13, HIGH);
    } 

    //if (serialCmd == "2") 
    //{
    //  digitalWrite(myOtherDevicePIN, HIGH);
    //} 
    //... and so on

    delay(numSecs.toInt() * 1000);
  }

  digitalWrite(13, LOW); //After numSecs reached, turn off the device
}
