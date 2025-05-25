import { useEffect, useState } from "react";
import PageBreadcrumb from "../components/common/PageBreadCrumb";
import UserMetaCard from "../components/UserProfile/UserMetaCard";
import UserInfoCard from "../components/UserProfile/UserInfoCard";
import UserAddressCard from "../components/UserProfile/UserAddressCard";
import PageMeta from "../components/common/PageMeta";
import { useAuth } from "../hooks/useAuth";
import axios from "axios";
import Button from "../components/ui/button/Button";
import { Modal } from "../components/ui/modal";

const BACKEND_URL = "http://localhost:5198";

export default function UserProfiles() {
  const { logout, token } = useAuth();
  const [user, setUser] = useState<any>(null);
  const [loading, setLoading] = useState(true);
  const [logoutModal, setLogoutModal] = useState(false);

  useEffect(() => {
    async function fetchUser() {
      setLoading(true);
      try {
        const res = await axios.get(`${BACKEND_URL}/api/Utilisateur/current-user`, {
          headers: { Authorization: `Bearer ${token}` },
        });
        console.log("Response from /api/Utilisateur/current-user:", res);
        // Defensive: check if backend returned HTML (error page) instead of JSON
        if (typeof res.data === "string" && res.data.startsWith("<!DOCTYPE html")) {
          console.error("Backend returned HTML instead of JSON. This usually means:");
          console.error("- The backend is not running, or");
          console.error("- The API endpoint is incorrect, or");
          console.error("- The request was routed to the frontend dev server instead of the backend.");
          setUser(null);
        } else {
          setUser(res.data);
          console.log("User info loaded:", res.data);
        }
      } catch (err) {
        setUser(null);
        console.error("Failed to fetch user info", err);
      }
      setLoading(false);
    }
    if (token) {
      fetchUser();
    } else {
      setUser(null);
      setLoading(false);
      console.warn("No token found, user is not authenticated.");
    }
  }, [token]);

  const handleUpdateUser = async (updated: any) => {
    if (!user) return;
    // Always send the current motDePasse and role from the loaded user object (frontend responsibility)
    const payload = {
      nom: updated.nom,
      prenom: updated.prenom,
      email: updated.email,
      motDePasse: user.motDePasse, // keep unchanged
      role: user.role,             // keep unchanged
    };
    console.log("Updating user with payload:", payload);
    try {
      const res = await axios.put(
        `${BACKEND_URL}/api/Utilisateur/UpdateUser/${user.id || user.Id}`,
        payload,
        {
          headers: { Authorization: `Bearer ${token}` },
        }
      );
      setUser(res.data);
      console.log("User info updated:", res.data);
    } catch (err: any) {
      console.error("Failed to update user:", err);
      alert(
        err?.response?.data
          ? `Update failed: ${JSON.stringify(err.response.data)}`
          : "Update failed: Bad Request"
      );
    }
  };

  const handleLogout = () => {
    logout(); // This should remove token from localStorage
    console.log("Token after logout:", localStorage.getItem("token")); // Should be null
    window.location.href = "/signin";
  };

  if (loading) return <div className="p-8 text-center">Loading...</div>;

  return (
    <>
      <PageMeta
        title="React.js Profile Dashboard | TailAdmin - Next.js Admin Dashboard Template"
        description="This is React.js Profile Dashboard page for TailAdmin - React.js Tailwind CSS Admin Dashboard Template"
      />
      <PageBreadcrumb pageTitle="Profile" />
      <div className="rounded-2xl border border-gray-200 bg-white p-5 dark:border-gray-800 dark:bg-white/[0.03] lg:p-6">
        <div className="flex justify-between items-center mb-5">
          <h3 className="text-lg font-semibold text-gray-800 dark:text-white/90 lg:mb-7 mb-0">
            Profile
          </h3>
          <Button
            size="sm"
            variant="outline"
            onClick={() => setLogoutModal(true)}
            className="!px-5"
          >
            Logout
          </Button>
        </div>
        <div className="space-y-6">
          <UserMetaCard user={user} onUpdate={handleUpdateUser} />
          <UserInfoCard user={user} />
          <UserAddressCard user={user} />
        </div>
      </div>
      <Modal isOpen={logoutModal} onClose={() => setLogoutModal(false)}>
        <div className="p-6">
          <h3 className="mb-4 text-lg font-semibold">Confirm Logout</h3>
          <p className="mb-6">Are you sure you want to logout?</p>
          <div className="flex gap-3 justify-end">
            <Button size="sm" variant="outline" onClick={() => setLogoutModal(false)}>
              Cancel
            </Button>
            <Button size="sm" onClick={handleLogout}>
              Logout
            </Button>
          </div>
        </div>
      </Modal>
    </>
  );
}
