### Role Design Decision

In this system, each user has exactly one role, stored as a simple `enum` (e.g., Admin, Doctor, Patient) within the `User` entity.

This approach is intentionally used to:

- Keep the architecture simple and maintainable
- Align with the current project scope (single-role per user)
- Avoid unnecessary complexity for a system that does not yet require dynamic role/permission management

### Why Not a Role Table or Many-to-Many Relationship?

While using a separate `Roles` table and supporting many-to-many relationships between users and roles is a more flexible and scalable design — it introduces extra complexity (e.g., user-role linking, role claims, dynamic permission resolution).

Since this system currently supports **one role per user**, the enum-based approach is cleaner, faster to implement, and fits the use case perfectly.

### Future Improvements

If the system grows to support features like:

- Assigning multiple roles to a user
- Managing roles dynamically from an admin interface
- Mapping fine-grained permissions per role

Then it can be extended to support a proper role-based access control (RBAC) model using:

- `Roles` table
- `UserRoles` many-to-many linking table
- Optional `Permissions` table (or Claims)

This would follow enterprise best practices for large-scale systems.

### 🧾 Doctor Account Creation & Password Handling

When an Admin creates a new Doctor account, the frontend is currently responsible for generating a **random temporary password**.

This password is not sent automatically by the system. Instead, the Admin is expected to **communicate it manually** to the Doctor (e.g., via email, phone, or other secure channels).

This approach keeps the system simple and avoids dependencies on external email providers during early development stages.

---

### 🚀 Planned Enhancement: Email-Based Account Activation

In future iterations of the system, we plan to implement a more secure and automated onboarding process that includes:

- Sending an activation link or temporary password to the Doctor’s registered email.
- Requiring the Doctor to set a new password on first login.
- Automatically locking the account until the email is verified.

This improvement aims to enhance security, streamline the onboarding process, and better reflect real-world production-ready practices.
