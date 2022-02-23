# PhoneBook
Basic .Net Core PhoneBook Web API

## Proje Amacı

Bu proje, basit bir telefon rehberi işlemleri yapılabilen REST API örneğidir.
* Kişi listeleme, ekleme, güncelleme ve silme işlemleri, 
* Kişiye ait iletişim bilgisi(Telefon, Email, Lokasyon) bilgilerini listeleme, ekleme, güncelleme ve silme işlemleri,
* Kişiye ait detaylı bilgilerin içerdiği verileri listeleme işlemleri,
* Konuma göre kaç kişinin olduğu ve kaç telefon numarası olduğu bilgilerini listeleyen işlemleri gerçekleştirir.

# REST API Request ve Response Bilgileri
- Contacts Request/Response

`GET /Contacts/`

    'Accept: application/json' https://localhost:44374/api/Contacts
    Response:  [{"Id": "6214062df05ae9ecd1d5a25e","UUID": 1,"Name": "Aynur","Surname": "Gökgöz","Firm": "MoonLight"}]

`POST /Contacts/`

    'Accept: application/json' https://localhost:44374/api/Contacts
     {  "Name": "","Surname": "", "Firm": "" }
    Response:  "Added Successfully"
    
`PUT /Contacts/`

    'Accept: application/json' https://localhost:44374/api/Contacts
     {  "UUID": 1, "Name": "", "Surname": "", "Firm": "" }
    Response:  "Updated Successfully"

`DELETE /Contacts/`

    'Accept: application/json' https://localhost:44374/Contacts/5
    Response:  "Deleted Successfully"
 
- ContactInformation Request/Response

`GET /ContactInformation/`

    'Accept: application/json' https://localhost:44374/api/ContactInformation
    Response: [{"Id": "6214b1f8e577a41211294f0d", "InfoId": 1,"ContactUUID": 1,"PhoneNumber": "05300000000", "Email": "AynurGokgoz@gmail.com", "Location": "İstanbul"}] 

`POST /ContactInformation/`

    'Accept: application/json' https://localhost:44374/api/ContactInformation
     { "ContactUUID": 1,"PhoneNumber": "","Email": "m@gmail.com", "Location": "İstanbul" }
      Response:  "Added Successfully"
    
`PUT /ContactInformation/`

    'Accept: application/json' https://localhost:44374/api/ContactInformation
     {"InfoId": 1, "ContactUUID": 1, "PhoneNumber": "", "Email": "m@gmail.com","Location": "İstanbul" }
      Response:  "Updated Successfully"
    
 `DELETE /Contacts/`

    'Accept: application/json' https://localhost:44374/ContactInformation/1
    Response:  "Deleted Successfully"

- ContactWithContactInformation Request

`GET /ContactWithContactInformation/`

    'Accept: application/json' https://localhost:44374/api/ContactInformation
     Response:  [{ "UUID": 1,"Name": "Aynur","Surname": "Gökgöz","Firm": "MoonLight","ContInfo": [{"Id": "6214b1f8e577a41211294f0d","InfoId": 1,"ContactUUID": 1, "PhoneNumber": "05300000000","Email": "a@gmail.com","Location": "İstanbul"}]]
    
- Report Request

`GET /Report/`

    'Accept: application/json' https://localhost:44374/api/Report/İstanbul
     Response: {"UUID":1,"SysTar":"Thursday, February 24, 2022","Status":"Tamamlandı","Location":"İstanbul","LocationContactCount":3,"LocationPhoneCount":3}
