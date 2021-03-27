﻿using LlamaTech.BE;
using LlamaTech.BL;
using System;
using System.Data;
using System.IO;
using System.Text;

namespace LlamaTech.web_admin
{
    public partial class frmUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                verDatos();
                cargarRol();
                estadoLimpio();
            }
        }

        public enum FormEstados
        {
            eAgregar = 1,
            eModificar = 2,
            eModificarPass = 3,
            eEliminar = 4,
            eLimpiar = 5
        }


        private void verDatos()
        {
            DataSet ds = new DataSet();
            UsuarioBL usuarioBL = new UsuarioBL();

            ds = usuarioBL.allUsers();

            if (ds.Tables.Count > 0)
            {
                rpGrid.DataSource = ds.Tables[0];
                rpGrid.DataBind();
            }

        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {

            if (HiddenField1.Value != "")
            {
                if (FormEstados.eAgregar.ToString() == HiddenField1.Value)
                {
                    if (txtEmail.Text != "" && txtContrasenia.Text != "" && txtNombre.Text != "")
                    {
                        agregaUsuario();
                        estadoLimpio();
                    } else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertError('Debe llenar todos los campos.')", true);
                    }
                    return;
                }
                else if (FormEstados.eModificar.ToString() == HiddenField1.Value)
                {
                    if (txtId.Text != "")
                    {
                        if (fileImg.HasFile)
                        {
                            modifica();
                            estadoLimpio();
                        }
                        else
                        {
                            modificaSinImg();
                            estadoLimpio();
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertError('El ID es obligatorio')", true);
                    }
                    return;
                }
                else if (FormEstados.eModificarPass.ToString() == HiddenField1.Value)
                {
                    if (txtId.Text != "")
                    {
                        modificarPass();
                        estadoLimpio();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertError('El ID es obligatorio')", true);
                    }
                    return;
                }
                else if (FormEstados.eEliminar.ToString() == HiddenField1.Value)
                {
                    if (txtId.Text != "")
                    {
                        elimina();
                        estadoLimpio();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertError('El ID es obligatorio')", true);
                    }
                    return;
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            estadoLimpio();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            estadoLimpio();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            estadoAgregar();
        }

        private void agregaUsuario()
        {
            DataSet ds = new DataSet();
            UsuarioBL usuarioBL = new UsuarioBL();
            UsuarioBE usuarioBE = new UsuarioBE();
            Security security = new Security();
            string hash = security.generarHash(txtContrasenia.Text);
            DateTime time = DateTime.Now;

            ds = usuarioBL.getEmail(txtEmail.Text);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertError('El correo ya existe!')", true);
            }
            else
            {
                try
                {

                    usuarioBE.Nombre = txtNombre.Text;
                    usuarioBE.Apellido = txtApellido.Text;
                    usuarioBE.Email = txtEmail.Text;
                    usuarioBE.Contraseña = hash;
                    usuarioBE.Telefono = txtTelefono.Text;
                    usuarioBE.FechaCreacion = time;
                    if (validarImagen(subirImagen()) == "")
                    {
                        usuarioBE.FotoPerfil = subirImagen();
                    }
                    else
                    {
                        string path = "../images/users/" + fileImg.FileName;
                        DirectoryInfo di = new DirectoryInfo(path);

                        foreach (FileInfo item in di.GetFiles())
                        {
                            item.Delete();
                        }

                        usuarioBE.FotoPerfil = subirImagen();
                    }
                    usuarioBE.IdRol = Convert.ToInt32(ddlRol.SelectedItem.Value);
                    usuarioBE.Status = chkStatus.Checked;

                    usuarioBL.agregarUser(usuarioBE);

                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertError('" + ex.Message + "')", true);
                }
                finally
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertAddSuccess('El usuario se agrego con exito')", true);

                }
            }



        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            estadoModificar();
        }

        private void modifica()
        {
            UsuarioBL usuarioBL = new UsuarioBL();
            UsuarioBE usuarioBE = new UsuarioBE();

            try
            {
                usuarioBE.IdUsuario = Convert.ToInt32(txtId.Text);
                usuarioBE.Nombre = txtNombre.Text;
                usuarioBE.Apellido = txtApellido.Text;
                usuarioBE.Email = txtEmail.Text;
                usuarioBE.Telefono = txtTelefono.Text;
                if (validarImagen(subirImagen()) == "")
                {
                    usuarioBE.FotoPerfil = subirImagen();
                }
                else
                {
                    string path = "../images/users/" + fileImg.FileName;
                    DirectoryInfo di = new DirectoryInfo(path);

                    foreach (FileInfo item in di.GetFiles())
                    {
                        item.Delete();
                    }

                    usuarioBE.FotoPerfil = subirImagen();
                }
                usuarioBE.IdRol = Convert.ToInt32(ddlRol.SelectedItem.Value);

                usuarioBL.modificarUser(usuarioBE);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertError('" + ex.Message + "')", true);
            }
            finally
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertUpdateSuccess('El usuario se modifico con exito')", true);

            }
        }
        private void modificaSinImg()
        {
            UsuarioBL usuarioBL = new UsuarioBL();
            UsuarioBE usuarioBE = new UsuarioBE();

            try
            {
                usuarioBE.IdUsuario = Convert.ToInt32(txtId.Text);
                usuarioBE.Nombre = txtNombre.Text;
                usuarioBE.Apellido = txtApellido.Text;
                usuarioBE.Email = txtEmail.Text;
                usuarioBE.Telefono = txtTelefono.Text;
                usuarioBE.IdRol = Convert.ToInt32(ddlRol.SelectedItem.Value);

                usuarioBL.modificarUserSinFoto(usuarioBE);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertError('" + ex.Message + "')", true);
            }
            finally
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertUpdateSuccess('El usuario se modifico con exito')", true);

            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            estadoEliminar();
        }
        private void elimina()
        {
            UsuarioBL usuarioBL = new UsuarioBL();
            UsuarioBE usuarioBE = new UsuarioBE();

            try
            {
                usuarioBE.IdUsuario = Convert.ToInt32(txtId.Text);

                usuarioBL.eliminarUser(usuarioBE.IdUsuario);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertError('" + ex.Message + "')", true);
            }
            finally
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertDelSuccess('El usuario se elimino con exito')", true);

            }
        }
        protected void btnModificarPass_Click(object sender, EventArgs e)
        {
            estadoModificarPass();
        }

        private void modificarPass()
        {
            if (txtId.Text != "" && txtContrasenia.Text != "")
            {
                UsuarioBL usuarioBL = new UsuarioBL();
                UsuarioBE usuarioBE = new UsuarioBE();
                Security security = new Security();
                string hash = security.generarHash(txtContrasenia.Text);

                try
                {
                    usuarioBE.IdUsuario = Convert.ToInt32(txtId.Text);
                    usuarioBE.Contraseña = hash;

                    usuarioBL.modificarUserPass(usuarioBE);
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertError('" + ex.Message + "')", true);
                }
                finally
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertUpdateSuccess('La contraseña se modifico con exito')", true);

                }
            }
        }




        // LLENAR DDL ROLES
        private void cargarRol()
        {
            DataSet ds = new DataSet();
            RolBL rolBL = new RolBL();

            ds = rolBL.rolActive();

            if (ds.Tables.Count > 0)
            {
                ddlRol.DataSource = ds.Tables[0];
                ddlRol.DataTextField = "Nombre";
                ddlRol.DataValueField = "IdRol";
                ddlRol.DataBind();
            }
        }


        // SUBIR IMAGEN
        private string subirImagen()
        {
            var ruta = "";

            if (fileImg.HasFile)
            {
                string extensionArchivo = System.IO.Path.GetExtension(fileImg.FileName).ToLower();
                string[] extensionPermitida = { ".jpg", ".jpeg", ".png", ".gif" };

                bool fileOk = false;

                for (int i = 0; i < extensionPermitida.Length; i++)
                {
                    if (extensionArchivo == extensionPermitida[i])
                    {
                        fileOk = true;
                    }
                }


                if (fileOk)
                {
                    string nombreImg = fileImg.FileName;
                    string codNombre = generarCodigo();
                    ruta = "../images/users/" + codNombre + nombreImg;
                    fileImg.SaveAs(Server.MapPath(ruta));
                    return ruta;
                }
            }

            return ruta;
        }
        private string validarImagen(string ruta)
        {
            string imagen = fileImg.FileName;
            string rutaImagen = ruta;
            string returnImagen;


            if (imagen == Path.GetFileName(rutaImagen))
            {
                return returnImagen = rutaImagen;
            }
            else
            {
                return returnImagen = "";
            }

        }

        

        // ESTADOS
        private void estadoAgregar()
        {
            HiddenField1.Value = FormEstados.eAgregar.ToString();
            txtId.Text = "";
            txtId.Enabled = false;
            txtNombre.Text = "";
            txtNombre.Enabled = true;
            txtApellido.Text = "";
            txtApellido.Enabled = true;
            txtEmail.Text = "";
            txtEmail.Enabled = true;
            txtContrasenia.Text = "";
            txtContrasenia.Enabled = true;
            txtTelefono.Text = "";
            txtTelefono.Enabled = true;
            fileImg.DataBind();
            ddlRol.SelectedIndex = 0;
            ddlRol.Enabled = true;
            imgUser.ImageUrl = "";
            chkStatus.Checked = false;
            chkStatus.Enabled = true;

            btnAgregar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;

            btnModificarPass.Enabled = false;

            btnAceptar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void estadoModificar()
        {
            HiddenField1.Value = FormEstados.eModificar.ToString();
            txtId.Enabled = true;
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            txtEmail.Enabled = true;
            txtContrasenia.Enabled = false;
            txtTelefono.Enabled = true;
            ddlRol.Enabled = true;
            chkStatus.Enabled = true;

            btnAgregar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;

            btnModificarPass.Enabled = false;

            btnAceptar.Enabled = true;
            btnCancelar.Enabled = true;
        }
        private void estadoModificarPass()
        {
            HiddenField1.Value = FormEstados.eModificarPass.ToString();
            txtId.Enabled = true;
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            txtEmail.Enabled = false;
            txtContrasenia.Enabled = true;
            txtTelefono.Enabled = false;
            ddlRol.Enabled = false;
            chkStatus.Enabled = false;

            btnAgregar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;

            btnModificarPass.Enabled = false;

            btnAceptar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void estadoEliminar()
        {
            HiddenField1.Value = FormEstados.eEliminar.ToString();
            txtId.Enabled = true;
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            txtEmail.Enabled = false;
            txtContrasenia.Enabled = false;
            txtTelefono.Enabled = false;
            ddlRol.Enabled = false;
            chkStatus.Enabled = false;

            btnAgregar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;

            btnModificarPass.Enabled = false;

            btnAceptar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void estadoLimpio()
        {
            HiddenField1.Value = FormEstados.eLimpiar.ToString();
            txtId.Text = "";
            txtId.Enabled = false;
            txtNombre.Text = "";
            txtNombre.Enabled = false;
            txtApellido.Text = "";
            txtApellido.Enabled = false;
            txtEmail.Text = "";
            txtEmail.Enabled = false;
            txtContrasenia.Text = "";
            txtContrasenia.Enabled = false;
            txtTelefono.Text = "";
            txtTelefono.Enabled = false;
            fileImg.DataBind();
            ddlRol.SelectedIndex = 0;
            ddlRol.Enabled = false;
            imgUser.ImageUrl = "";
            chkStatus.Checked = false;
            chkStatus.Enabled = false;

            btnAgregar.Enabled = true;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;

            btnModificarPass.Enabled = true;

            btnAceptar.Enabled = false;
            btnCancelar.Enabled = false;

            verDatos();
        }

        

        // GENERAR CODIGO ALEATORIO 
        private string generarCodigo()
        {
            string s = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random r = new Random();
            StringBuilder sb = new StringBuilder();

            for (int i = 1; i < 20; i++)
            {
                int count = r.Next(0, s.Length - 1);
                sb.Append(s.Substring(count, 1));
            }

            return sb.ToString();
        }
    }
}