# Use Cases — Booking API

## 1. User Registration

**Actor:** Guest  
**Preconditions:** User is not registered  

**Main flow:**
1. User sends email and password
2. System validates email format
3. System checks if email is unique
4. System creates a new user
5. System returns success response (200 OK)

**Alternative flows:**
- Invalid email → 400 Bad Request  
- Email already exists → 409 Conflict  

---

## 2. User Login

**Actor:** User  
**Preconditions:** User is registered  

**Main flow:**
1. User sends email and password
2. System validates credentials
3. System generates JWT token
4. System returns token (200 OK)

**Alternative flows:**
- Invalid credentials → 401 Unauthorized  

---

## 3. Create Booking

**Actor:** Authorized User  
**Preconditions:** User is authenticated  

**Main flow:**
1. User sends StartTime, EndTime, Description
2. System validates:
   - StartTime > current time
   - EndTime > StartTime
3. System checks for time conflicts
4. System creates booking
5. System returns created booking (200 OK)

**Alternative flows:**
- StartTime in the past → 400 Bad Request  
- Invalid time range → 400 Bad Request  
- Time conflict → 409 Conflict  

---

## 4. Get User Bookings

**Actor:** Authorized User  
**Preconditions:** User is authenticated  

**Main flow:**
1. User requests bookings list
2. System retrieves all bookings for this user
3. System returns list (200 OK)

---

## 5. Update Booking

**Actor:** Authorized User  
**Preconditions:** Booking exists  

**Main flow:**
1. User sends updated StartTime, EndTime, Description
2. System validates data
3. System checks for conflicts
4. System updates booking
5. System returns updated booking (200 OK)

**Alternative flows:**
- Booking not found → 404 Not Found  
- Invalid data → 400 Bad Request  
- Time conflict → 409 Conflict  

---

## 6. Delete Booking

**Actor:** Authorized User  
**Preconditions:** Booking exists  

**Main flow:**
1. User sends delete request
2. System deletes booking
3. System returns success (200 OK)

**Alternative flows:**
- Booking not found → 404 Not Found  
